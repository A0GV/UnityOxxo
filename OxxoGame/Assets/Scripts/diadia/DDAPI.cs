using UnityEngine;
using UnityEngine.Networking; 
using Newtonsoft.Json; 
using System.Collections.Generic;
using System.Collections;

public class DDAPI : MonoBehaviour
{
    public DiaControl diaControl; // Instance de DiaControl

    // Usa API para checar diasJugados y mandarlo al DiaControl
    void Awake()
    {
        StartCoroutine(GetDaysPlayed());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Cantidad de días que ha jugado el usuario logeado 
    IEnumerator GetDaysPlayed() 
    {
        // Usa id de jugador para checar su exp, cambiar jugando si están en otro juego. Cabmiar diaControl cuando funcione validación de cuenta de usuario
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
            //UnityEngine.Debug.Log("Dias jugados: " + diaControl.diasJugados);
        }
    }

    // Para post datos de jugada en historial y usuario_historial 
    public IEnumerator PostDatosJuego()
    {
        string url = "https://localhost:7119/manageCurrency/AgregarDatosJuego";

        // Use form data para agregar los datos al JSON
        WWWForm form = new WWWForm();
        form.AddField("id_usuario", diaControl.id_usuario);
        form.AddField("id_juego", 3);
        form.AddField("monedas", diaControl.elotesGanados);
        form.AddField("exp", diaControl.expGanado);

        UnityWebRequest request = UnityWebRequest.Post(url, form); // Para hacer un post 
        request.certificateHandler = new ForceAcceptAll(); // Para accept all de integradora
        yield return request.SendWebRequest();

        // Ver si funciona
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error al enviar datos: " + request.error);
        }
        else
        {
            Debug.Log("Se agregó todo :D");
        }
    }
}