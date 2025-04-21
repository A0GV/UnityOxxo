using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking; 
using Newtonsoft.Json; 
using System.Collections.Generic;
using System.Collections;
using System;

public class MenuScene : MonoBehaviour
{  
    // Sonido
    private AudioSource audioSource;
    public AudioClip buttonSound;
    public Text eloteText;
    public Text expText;
    public Text rachaText; 
    int idUsuario ; // ID del usuario, se asigna al iniciar sesión

    // Poner sonido
    void Awake()
    {
        // Configuración inicial del audio
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;

        if (LoginAPI.UserId.HasValue)
        {
            idUsuario = LoginAPI.UserId.Value; // Asigna el valor si existe
            Debug.Log($"ID de usuario asignado: {idUsuario}");
        }
        else
        {
            Debug.Log("No hay id usuario" + idUsuario); 
        }

        // Gets valores de usuario
        StartCoroutine(GetElotes());
        StartCoroutine(GetExp());
        StartCoroutine(GetRacha());
    }

    // Manda a escena de juego
    public void StartToPlay(string sceneName) 
    {
        StartCoroutine(PlaySoundAndLoad(sceneName));
    }

    //Manda a la escena de juego seleccionada
    IEnumerator PlaySoundAndLoad(string sceneName)
    {
        audioSource.PlayOneShot(buttonSound, 0.1f);
        yield return new WaitForSeconds(buttonSound.length); // Para esperar a q suene el botón antes de cambiar de escena
        SceneManager.LoadScene(sceneName);
    }

    // Exits the game application
    public void ExitGame(){
        UnityEditor.EditorApplication.isPlaying = false;
        //Application.Quit();
    }

    // Gets elotes totales
    IEnumerator GetElotes() 
    {
        // Usa id de jugador para checar su exp, cambiar jugando si están en otro juego. Cabmiar diaControl cuando funcione validación de cuenta de usuario
        string JSONurl = "https://localhost:7119/manageCurrency/GetElotesTotal?id_logged=" + idUsuario; // Cambiar id_usuario
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
            elotes = JsonConvert.DeserializeObject<int>(web.downloadHandler.text); // Necesitas especificar q está usando un <int> para deserialize
        }

        Debug.Log("Elotes: " + elotes);

        eloteText.text = elotes.ToString();
    }

    // Gets exp total
    IEnumerator GetExp() 
    {
        // Usa id de jugador para checar su exp, cambiar jugando si están en otro juego. Cabmiar diaControl cuando funcione validación de cuenta de usuario
        string JSONurl = "https://localhost:7119/manageCurrency/GetExpTotal?id_logged=" + idUsuario; // Cambiar id_usuario
        UnityWebRequest web = UnityWebRequest.Get(JSONurl); 
        web.certificateHandler = new ForceAcceptAll();
        yield return web.SendWebRequest();

        int exp = 0;

        if (web.result != UnityWebRequest.Result.Success)
        {
            UnityEngine.Debug.Log("Error API: " + web.error);
        }
        else
        {
            exp = JsonConvert.DeserializeObject<int>(web.downloadHandler.text); // Necesitas especificar q está usando un <int> para deserialize
        }
        expText.text = exp.ToString();
    }

    // Gets racha total
    IEnumerator GetRacha() 
    {
        // Usa id de jugador para checar su exp, cambiar jugando si están en otro juego. Cabmiar diaControl cuando funcione validación de cuenta de usuario
        string JSONurl = "https://localhost:7119/manageCurrency/GetRacha?id_logged=" + idUsuario; // Cambiar id_usuario
        UnityWebRequest web = UnityWebRequest.Get(JSONurl); 
        web.certificateHandler = new ForceAcceptAll();
        yield return web.SendWebRequest();

        int racha = 0;

        if (web.result != UnityWebRequest.Result.Success)
        {
            UnityEngine.Debug.Log("Error API: " + web.error);
        }
        else
        {
            racha = JsonConvert.DeserializeObject<int>(web.downloadHandler.text); // Necesitas especificar q está usando un <int> para deserialize
        }
        Debug.Log(racha);
        rachaText.text = racha.ToString();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
