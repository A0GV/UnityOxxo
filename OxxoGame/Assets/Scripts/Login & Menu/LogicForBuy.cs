using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections;
using System;

public class LogicForBuy : MonoBehaviour
{
    public static usuario_skin[] skinsArray;
    public static int elotesstore { get; set; }
    public GameObject[] skinPanels; //este es el de poner
    public GameObject[] quitarPanels;
    public GameObject[] comprarPanels;

    IEnumerator GetElotes()
    {
        // Usa id de jugador para checar su exp, cambiar jugando si están en otro juego. Cabmiar diaControl cuando funcione validación de cuenta de usuario
        string JSONurl = "https://localhost:7119/manageCurrency/GetElotesTotal?id_logged=" + LoginAPI.UserId.Value;
        UnityWebRequest web = UnityWebRequest.Get(JSONurl);
        web.certificateHandler = new ForceAcceptAll();
        yield return web.SendWebRequest();

        int elotes = 0;

        if (web.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error API: " + web.error);
        }
        else
        {
            elotesstore = JsonConvert.DeserializeObject<int>(web.downloadHandler.text); // Necesitas especificar q está usando un <int> para deserialize
        }

        Debug.Log("el GetElotes de LogicForBuy se activo");


        Debug.Log("Elotes: " + elotes);
    }
    IEnumerator GetSkinsbuyed()
    {
        string JSONurl = $"https://localhost:7119/Login/AlldatosSkin?userId={LoginAPI.UserId}";
        UnityWebRequest web = UnityWebRequest.Get(JSONurl);
        web.certificateHandler = new ForceAcceptAll();
        yield return web.SendWebRequest();

        if (web.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error API: " + web.error);
        }
        else
        {
            // Deserializa la lista de skins
            List<usuario_skin> skins = JsonConvert.DeserializeObject<List<usuario_skin>>(web.downloadHandler.text);
            skinsArray = skins.ToArray();
            // Debug.Log("Skins recibidas: " + skins.Count);
            // foreach (var skin in skinsArray)
            // {
            //     Debug.Log($"Skin ID: {skin.id_skin}, IsActive: {skin.isActive}");
            // }
            ActualizarPaneles();
        }
    }

    public void ActualizarPaneles()
    {


        for (int i = 0; i < skinsArray.Length; i++)
        {
            var skin = skinsArray[i];
            int idx = skin.id_skin; // Ajusta si tus IDs no empiezan en 1


            if (skin.isActive == 1)
            {
                comprarPanels[idx].SetActive(false);
                // Si está puesta, muestra el panel de quitar
                if (quitarPanels[idx] != null) quitarPanels[idx].SetActive(true);
            }
            else if (skin.isActive == 0)
            {
                comprarPanels[idx].SetActive(false);
                // Si está comprada pero no puesta, muestra el panel de poner
                if (skinPanels[idx] != null) skinPanels[idx].SetActive(true);
            }
        }
    }
    void Awake()
    {
        StartCoroutine(GetSkinsbuyed());
        StartCoroutine(GetElotes());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void puedeComprar(int idSkin)
    {

    }
}
