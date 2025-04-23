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
    public static int elotesstore { get; set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IEnumerator GetElotes()
        {
            // Usa id de jugador para checar su exp, cambiar jugando si están en otro juego. Cabmiar diaControl cuando funcione validación de cuenta de usuario
            string JSONurl = "https://localhost:7119/manageCurrency/GetElotesTotal?id_logged=" + LoginAPI.UserId; // Cambiar id_usuario
            UnityWebRequest web = UnityWebRequest.Get(JSONurl);
            web.certificateHandler = new ForceAcceptAll();
            yield return web.SendWebRequest();

            int elotes = 0;

            if (web.result != UnityWebRequest.Result.Success)
            {
                UnityEngine.Debug.Log("Error API: " + web.error);
            }
            else
            {
                elotesstore = JsonConvert.DeserializeObject<int>(web.downloadHandler.text); // Necesitas especificar q está usando un <int> para deserialize
            }

            Debug.Log("Elotes: " + elotes);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void puedeComprar(int idSkin)
    {

    }
}
