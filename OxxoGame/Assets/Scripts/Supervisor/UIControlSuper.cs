using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIControlSuper : MonoBehaviour
{
    public GameObject main;
    public GameObject manual;
    public GameObject pregunta;
    public GameObject resumen;
    public GameObject respuesta;

    void Start()
    {

    }

    public void StartTime(){
    resumen.SetActive(false);  // Ensure the panel is hidden at the start
    StartCoroutine(MostrarResumen());
    }

    IEnumerator MostrarResumen(){
        yield return new WaitForSeconds(60); // Wait 60 secs before first appearance
        ShowResumen();
    }

    public void ShowResumen(){
        ShowPanel("resumen");
    }

    public void ShowMain(){
        ShowPanel("main");
    }


    public void ShowPanel(string panelName)
    {
        // Hide all panels first
        main.SetActive(false);
        manual.SetActive(false);
        pregunta.SetActive(false);
        resumen.SetActive(false);
        respuesta.SetActive(false);

        // Show the selected panel
        switch (panelName)
        {
            case "main":
                main.SetActive(true);
                break;
            case "manual":
                manual.SetActive(true);
                break;
            case "pregunta":
                pregunta.SetActive(true);
                break;
            case "respuesta":
                respuesta.SetActive(true);
                break;
            case "resumen":
                resumen.SetActive(true);
                break;
        }
    }

    public void ShowResultados()
    {
        SuperControl.Instance.Gotoendgame();
    }

    public void Gotomenu()
    {
        SuperControl.Instance.EndMiniGame();
    }
}
