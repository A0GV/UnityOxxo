using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using Newtonsoft;
using Newtonsoft.Json;
using UnityEngine.Rendering.Universal;


public class UIControlSuper : MonoBehaviour
{
    // References to different UI panels
    public GameObject main;
    public GameObject manual;
    public GameObject pregunta;
    public GameObject resumen;
    public GameObject respuesta;
    public GameObject pausa;

    public GameObject resumenExtendido;
    public string[] textosFinal;
    public int malos;
    public int buenos;

    public int elotes;
    public Text elotesFinalText;
    public int burbujas;
    public Text burbujasFinalText;
    public Text textElotePausa;
    public Text textBurbujaPausa;
    public Text speech;

    public Text Resume;
    public Text Points;

    public Text ResuemnExt;
    private Coroutine resumenCoroutine;
    private bool isPaused = false; 
    static public npcController Instance;
    private IndividualBehaviourNpc instance; 
    private int id_usuario;

    void Awake()
    {
        if (LoginAPI.UserId.HasValue)
        {
            id_usuario = LoginAPI.UserId.Value; 
            Debug.Log($"ID de usuario asignado: {id_usuario}");
        }
        else
        {
            Debug.Log("UserId no está disponible. Asegúrate de haber iniciado sesión correctamente.");
        }
    }
    void Start()
    {
        textosFinal = new string[8];
        instance = Object.FindFirstObjectByType<IndividualBehaviourNpc>();

    }


    public void ShowMain()
    {
        main.SetActive(true);
        resumen.SetActive(false);
        manual.SetActive(false);
        pausa.SetActive(false);
        respuesta.SetActive(false);
    }

    public void ShowResumen()
    {
        main.SetActive(false);
        resumen.SetActive(true);
        StartCoroutine(EnviarRecompensas());
        string finalTxt = $"{buenos}/6 aciertos\n  {malos}/6 errores de aceptación \n Si quieres ver por qué, pulsa el botón de siguiente.";
        elotesFinalText.text = elotes.ToString();
        burbujasFinalText.text = burbujas.ToString();
        StartCoroutine(npcController.Instance.textoAnimado(finalTxt, Resume, npcController.Instance.speedLocal)); //Accedo a la instancia del; singleton y de ahi, llamo al metodo textoAnimado
    }

    public void ShowResumenExtendidio()
    {
        main.SetActive(false);
        resumen.SetActive(false);
        resumenExtendido.SetActive(true);
        string finalText = string.Join("\n", textosFinal);
        StartCoroutine(npcController.Instance.textoAnimado(finalText, ResuemnExt, npcController.Instance.speedLocal));

    }

    public void ShowPanel(string panelName)
    {
        manual.SetActive(false);
        pregunta.SetActive(false);
        resumen.SetActive(false);
        respuesta.SetActive(false);
        pausa.SetActive(false);
        resumenExtendido.SetActive(false);

        switch (panelName)
        {
            case "ManualCanvas":
                manual.SetActive(true);
                break;
            case "QuestionCanvas":
                pregunta.SetActive(true);
                break;
            case "ResponseCanvas":
                respuesta.SetActive(true);
                main.SetActive(true); 
                break;
            case "PauseCanvas":
                PauseGame(); 
                break;
        }
    }

    public void PauseGame()
    {

        isPaused = true; 
        pausa.SetActive(true); // Show pause panel
        Time.timeScale = 0; // Freeze game physics and coroutines
        if (textElotePausa != null)
        {
            textElotePausa.text = elotes.ToString();
        }
        if (textBurbujaPausa != null)
        {
            textBurbujaPausa.text = burbujas.ToString();
        }
        if (resumenCoroutine != null)
        {
            StopCoroutine(resumenCoroutine); // Stop the coroutine
            resumenCoroutine = null; // Reset coroutine reference
        }
    }

    // Resumes the game and restarts the summary coroutine
    public void ResumeGame()
    {
        isPaused = false; // Unpause the game
        pausa.SetActive(false); // Hide pause panel
        Time.timeScale = 1; // Resume normal time
    }

    public void RestartGame()
    {
        Time.timeScale = 1; // Ensure time is running
        SceneManager.LoadScene("SupervisorScene"); // Reload the current scene
    }

    // Calls the method to transition to the endgame scene
    public void ShowResultados()
    {
        SuperControl.Instance.Gotoendgame();
    }

    // Calls the method to return to the menu scene
    public void Gotomenu()
    {
        StartCoroutine(EnviarRecompensas());
        Time.timeScale = 1;

        SuperControl.Instance.EndMiniGame();
    }

    public void ShowResponse(int numPregunta)
    {
        string respuestaTexto;

        // Obtener la respuesta según la pregunta seleccionada
        switch (numPregunta)
        {
            case 1:
                respuestaTexto = PlayerPrefs.GetString("RespuestaUno");
                break;
            case 2:
                respuestaTexto = PlayerPrefs.GetString("RespuestaDos");
                break;
            case 3:
                respuestaTexto = PlayerPrefs.GetString("RespuestaTres");
                break;
            default:
                respuestaTexto = "Pregunta no válida";
                break;
        }

        // Mostrar el canvas de respuesta
        talkAxolotl();
        ShowPanel("ResponseCanvas");
        Debug.Log(respuestaTexto);
        StartCoroutine(npcController.Instance.textoAnimado(respuestaTexto, speech, npcController.Instance.speedLocal));
    }

    public void decisiones(int decision)
    {
        Debug.Log($"Decisiones casteada, se llamo como {decision}");
        int esBueno = PlayerPrefs.GetInt("esBueno");
        string texto = PlayerPrefs.GetString("malaDecision");
        string local = PlayerPrefs.GetString("local");
        if (esBueno != decision)
        {

            malos++;
            textosFinal[malos] = $"{local}: {texto}";
            Debug.Log("Malos tiene el valor de" + malos);
        }
        else
        {
            buenos++;
            burbujas = burbujas + 20;
            elotes = elotes + 4;
            Debug.Log("Buenos, tiene el valod de" + buenos);
        }
    }

    void talkAxolotl()
    {
        Debug.Log("Se llamo a callNpc");

        // Intenta usar primero la referencia del singleton
        if (IndividualBehaviourNpc.instance != null)
        {
            IndividualBehaviourNpc.instance.callTalk();
        }
        // Si no existe, busca de nuevo en la escena (como fallback)
        else if (instance != null)
        {
            instance.callTalk();
        }
        // Si tampoco existe la referencia local, intenta buscarla una última vez
        else
        {
            var foundInstance = Object.FindFirstObjectByType<IndividualBehaviourNpc>();
            if (foundInstance != null)
            {
                foundInstance.callTalk();
                // Actualiza la referencia local para próximas llamadas
                instance = foundInstance;
            }
            else
            {
                Debug.LogError("No hay referencia al componente IndividualBehaviourNpc para llamar a callTalk()");
            }
        }
    }

    public IEnumerator EnviarRecompensas()
    {
        string url = "https://localhost:7119/manageCurrency/AgregarDatosJuego";

        // Crear el formulario para enviar los datos
        WWWForm form = new WWWForm();
        form.AddField("id_usuario", id_usuario);
        form.AddField("id_juego", 1); // ID del juego Supervisor
        form.AddField("monedas", elotes);
        form.AddField("exp", burbujas);

        UnityWebRequest request = UnityWebRequest.Post(url, form);
        request.certificateHandler = new ForceAcceptAll(); // Utilizar la clase existente para certificados
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error al enviar recompensas: " + request.error);
        }
        else
        {
            Debug.Log($"Recompensas enviadas: {elotes} elotes y {burbujas} burbujas");
        }
    }

}