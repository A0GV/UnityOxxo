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
        StartCoroutine(MostrarResumen());
    }

    IEnumerator MostrarResumen(){
        yield return new WaitForSeconds(8);
        ShowResumen();
        StartCoroutine(MostrarResumen());
    }

    public void ShowMain(){
        main.SetActive(true);
        resumen.SetActive(false);
        manual.SetActive(false);
    }
    public void ShowResumen(){
        main.SetActive(false);
        resumen.SetActive(true);
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
