using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIControlSuper : MonoBehaviour
{
    // References to different UI panels
    public GameObject main;
    public GameObject manual;
    public GameObject pregunta;
    public GameObject resumen;
    public GameObject respuesta;
    public GameObject pausa;


    void Start()
    {
    
    }

    // Starts the coroutine to periodically show the summary panel
    public void StartTime()
    {
        StartCoroutine(MostrarResumen());
    }

    // Coroutine that waits for 8 seconds and then shows the summary panel repeatedly
    IEnumerator MostrarResumen()
    {
        yield return new WaitForSeconds(8); // Waits 8 seconds
        ShowResumen(); // Shows the summary panel
        StartCoroutine(MostrarResumen()); // Calls itself again to keep looping
    }

    // Displays the main panel and hides other panels
    public void ShowMain()
    {
        main.SetActive(true);
        resumen.SetActive(false);
        manual.SetActive(false);
        pausa.SetActive(false);
    }

    // Displays the summary panel and hides the main panel
    public void ShowResumen()
    {
        main.SetActive(false);
        resumen.SetActive(true);
    }

    // Controls the visibility of different UI panels based on the panel name
    public void ShowPanel(string panelName)
    {
        // Hide all panels first
        main.SetActive(false);
        manual.SetActive(false);
        pregunta.SetActive(false);
        resumen.SetActive(false);
        respuesta.SetActive(false);

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
                pausa.SetActive(true);
                main.SetActive(true);
                break;

        }
    }

    // Calls the method to transition to the endgame scene
    public void ShowResultados()
    {
        SuperControl.Instance.Gotoendgame();
    }

    // Calls the method to return to the menu scene
    public void Gotomenu()
    {
        SuperControl.Instance.EndMiniGame();
    }
}
