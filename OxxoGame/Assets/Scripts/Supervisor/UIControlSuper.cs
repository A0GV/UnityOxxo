using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class UIControlSuper : MonoBehaviour
{
    // References to different UI panels
    public GameObject main;
    public GameObject manual;
    public GameObject pregunta;
    public GameObject resumen;
    public GameObject respuesta;
    public GameObject pausa;

    private Coroutine resumenCoroutine; // Stores the coroutine reference
    private bool isPaused = false; // Tracks if the game is paused

    void Start()
    {
        StartTime(); // Start the summary coroutine on game start
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
        manual.SetActive(false);
        pregunta.SetActive(false);
        resumen.SetActive(false);
        respuesta.SetActive(false);
        pausa.SetActive(false);

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
}