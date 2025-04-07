using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
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
    public int burbujas;

    public Text speech;

    public Text Resume;
    public Text Points;

    public Text ResuemnExt;

    private Coroutine resumenCoroutine; // Stores the coroutine reference
    private bool isPaused = false; // Tracks if the game is paused
    static public npcController Instance;



    void Start()
    {
        textosFinal = new string[8];
    }

    // Starts the coroutine to periodically show the summary panel
    public void StartTime()
    {
        if (resumenCoroutine == null)
        {
            // resumenCoroutine = StartCoroutine(MostrarResumen()); // Start the coroutine
        }
    }

    // Coroutine that waits for 8 seconds and then shows the summary panel repeatedly
    IEnumerator MostrarResumen()
    {
        while (true) // Infinite loop, stops only when paused
        {
            yield return new WaitForSeconds(8); // Waits 8 seconds
            if (!isPaused) // Only show resumen if the game is not paused
            {
                ShowResumen(); // Shows the summary panel
            }
        }
    }

    // Displays the main panel and hides other panels
    public void ShowMain()
    {
        main.SetActive(true);
        resumen.SetActive(false);
        manual.SetActive(false);
        pausa.SetActive(false);
        respuesta.SetActive(false);
    }

    // Displays the summary panel and hides the main panel
    public void ShowResumen()
    {
        main.SetActive(false);
        resumen.SetActive(true);
        string finalTxt=$"Tuviste\n{buenos}/7 aciertos\n  {malos}/7\n Si quieres ver porque. Presiona el boton de siguiente.";
        string points=$"Conseguiste {burbujas} burbujas y {elotes} elotes";
        StartCoroutine(npcController.Instance.textoAnimado(finalTxt,Resume,npcController.Instance.speedLocal)); //Accedo a la instancia del; singleton y de ahi, llamo al metodo textoAnimado
        StartCoroutine(npcController.Instance.textoAnimado(points,Points,npcController.Instance.speedLocal)); //Accedo a la instancia del; singleton y de ahi, llamo al metodo textoAnimado

    }

    public void ShowResumenExtendidio()
    {
        main.SetActive(false);
        resumen.SetActive(false);
        resumenExtendido.SetActive(true);
        string finalText = string.Join("\n", textosFinal);
        StartCoroutine(npcController.Instance.textoAnimado(finalText, ResuemnExt, npcController.Instance.speedLocal));

    }

    // Controls the visibility of different UI panels based on the panel name
    public void ShowPanel(string panelName)
    {
        // Hide all panels first
        manual.SetActive(false);
        pregunta.SetActive(false);
        resumen.SetActive(false);
        respuesta.SetActive(false);
        pausa.SetActive(false);
        resumenExtendido.SetActive(false);

        // Show the selected panel based on the provided name
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
                main.SetActive(true); // Ensures the main panel remains active with the response panel
                break;
            case "PauseCanvas":
                PauseGame(); // Activates pause mode
                break;
        }
    }

    // Pauses the game and stops the summary coroutine
    public void PauseGame()
    {
        isPaused = true; // Set game as paused
        pausa.SetActive(true); // Show pause panel
        Time.timeScale = 0; // Freeze game physics and coroutines
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
        StartTime(); // Restart the coroutine
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
        ShowPanel("ResponseCanvas");
        Debug.Log(respuestaTexto);
        StartCoroutine(npcController.Instance.textoAnimado(respuestaTexto, speech, npcController.Instance.speedLocal));
    }

    public void decisiones(int decision)
    {
        Debug.Log($"Decisiones casteada, se llamo como {decision}");
        int esBueno=PlayerPrefs.GetInt("esBueno");
        string texto=PlayerPrefs.GetString("malaDecision");
        string local=PlayerPrefs.GetString("local");
        if (esBueno!=decision)
        {

            malos++;
            textosFinal[malos] = $"{local}: {texto}";
            Debug.Log("Malos tiene el valor de"+malos);
        }
        else
        {
            buenos++;
            burbujas=burbujas+20;
            elotes=elotes+40;
            Debug.Log("Buenos, tiene el valod de"+buenos);
        }
    }
}