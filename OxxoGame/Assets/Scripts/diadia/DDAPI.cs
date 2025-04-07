using UnityEngine;
using UnityEngine.Networking; 
using Newtonsoft.Json; 
using System.Collections.Generic;
using System.Collections;

public class DDAPI : MonoBehaviour
{
    public DiaControl diaControl; // Instance de DiaControl
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(GetDaysPlayed());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GetDaysPlayed() 
    {
        // Usa id de jugador para checar su exp, cambiar jugando si están en otro juego
        string JSONurl = "https://localhost:7119/manageCurrency/GetDaysPlayed?id_logged=" + diaControl.id_usuario + "&id_jugando=3";
        UnityEngine.Debug.Log("Connected to localhost"); // Daba error otherwise?
        UnityWebRequest web = UnityWebRequest.Get(JSONurl); 
        web.certificateHandler = new ForceAcceptAll();
        yield return web.SendWebRequest();

        if (web.result != UnityWebRequest.Result.Success)
        {
            UnityEngine.Debug.Log("Error API: " + web.error);
        }
        else
        {
            diaControl.diasJugados = JsonConvert.DeserializeObject<int>(web.downloadHandler.text); // Necesitas especificar q está usando un <int> para deserialize
            UnityEngine.Debug.Log("Dias jugados: " + diaControl.diasJugados);
            /*
            List<Libros> listaLibros = new List<Libros>();
            listaLibros = 
            LoadBookInfo(BookSelection, listaLibros); 
            PlayerPrefs.SetInt("book_no", BookSelection);
            */
        }
    }
}
