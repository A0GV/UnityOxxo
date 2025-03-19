using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIControlEnemigos : MonoBehaviour
{
    public GameObject Apuntar;
    public GameObject Pregunta;
    public GameObject RespuestaCorrecta;
    public GameObject RespuestaIncorrecta;
    public GameObject Ganar;
    public GameObject Perder;

    void Start()
    {

    }

    public void StartTime(){
        StartCoroutine(MostrarApuntar());
    }

    IEnumerator MostrarApuntar(){
        yield return new WaitForSeconds(60); // Wait 60 secs before first appearance
        ShowApuntar();
        StartCoroutine(MostrarApuntar());
    }

    public void ShowApuntar(){
        Apuntar.SetActive(true);
        Pregunta.SetActive(false);
        RespuestaCorrecta.SetActive(false);
        RespuestaIncorrecta.SetActive(false);
        Ganar.SetActive(false);
        Perder.SetActive(false);
    }
    public void ShowPregunta(){
        Apuntar.SetActive(false);
        Pregunta.SetActive(true);
        RespuestaCorrecta.SetActive(false);
        RespuestaIncorrecta.SetActive(false);
        Ganar.SetActive(false);
        Perder.SetActive(false);
    }

     public void ShowRespuestaCorrecta(){
        Apuntar.SetActive(false);
        Pregunta.SetActive(false);
        RespuestaCorrecta.SetActive(true);
        RespuestaIncorrecta.SetActive(false);
        Ganar.SetActive(false);
        Perder.SetActive(false);
    }


     public void ShowRespuestaIncorrecta(){
        Apuntar.SetActive(false);
        Pregunta.SetActive(false);
        RespuestaCorrecta.SetActive(false);
        RespuestaIncorrecta.SetActive(true);
        Ganar.SetActive(false);
        Perder.SetActive(false);
    }

     public void ShowGanar(){
        Apuntar.SetActive(false);
        Pregunta.SetActive(false);
        RespuestaCorrecta.SetActive(false);
        RespuestaIncorrecta.SetActive(false);
        Ganar.SetActive(true);
        Perder.SetActive(false);
    }

      public void ShowPerder(){
        Apuntar.SetActive(false);
        Pregunta.SetActive(false);
        RespuestaCorrecta.SetActive(false);
        RespuestaIncorrecta.SetActive(false);
        Ganar.SetActive(false);
        Perder.SetActive(true);
    }

    public void ShowPanel(string panelName)
    {
        // Hide all panels first
        Apuntar.SetActive(false);
        Pregunta.SetActive(false);
        RespuestaCorrecta.SetActive(false);
        RespuestaIncorrecta.SetActive(false);
        Ganar.SetActive(false);
        Perder.SetActive(false);

        // Show the selected panel
        switch (panelName)
        {
            case "ApuntarPanel":
                Apuntar.SetActive(true);
                break;
            case "PreguntaPanel":
                Pregunta.SetActive(true);
                break;
            case "RespuestaCorrecta":
                RespuestaCorrecta.SetActive(true);
                break;
            case "RespuestaIncorrecta":
                RespuestaIncorrecta.SetActive(true);
                break;  
            case "Ganar":
                Ganar.SetActive(true);
                break;
            case "Perder":
                Perder.SetActive(true);
                break;              
        }
    }

    public void ShowResultados()
    {
        EnemigosControl.Instance.Gotoendgame();
    }

    public void Gotomenu()
    {
        EnemigosControl.Instance.EndMiniGame();
    }
}
