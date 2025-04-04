using UnityEngine;
using UnityEngine.UI;

public class UIControlEnemigos : MonoBehaviour
{
    // Paneles de UI
    public GameObject Estatus;
    public GameObject Pregunta;
    public GameObject RespuestaCorrecta;
    public GameObject RespuestaIncorrecta;
    public GameObject Pausa;
    public GameObject Perder;

    // Elementos de UI para balas y vidas
    public Text balasTexto;  // Texto que muestra la cantidad de balas
    public Image[] vidasImagenes;  // Array de imágenes de corazones

    // Método para mostrar un panel específico
    public void ShowPanel(string panelName)
    {
        // Ocultar todos los paneles primero
        Estatus.SetActive(false);
        Pregunta.SetActive(false);
        RespuestaCorrecta.SetActive(false);
        RespuestaIncorrecta.SetActive(false);
        Pausa.SetActive(false);
        Perder.SetActive(false);

        // Mostrar el panel seleccionado
        switch (panelName)
        {
            case "Estatus":
                Estatus.SetActive(true);
                break;
            case "Pregunta":
                Pregunta.SetActive(true);
                break;
            case "RespuestaCorrecta":
                RespuestaCorrecta.SetActive(true);
                break;
            case "RespuestaIncorrecta":
                RespuestaIncorrecta.SetActive(true);
                break;
            case "Ganar":
                Pausa.SetActive(true);
                break;
            case "Perder":
                Perder.SetActive(true);
                break;
        }
    }

    // Actualizar el número de balas en la UI
    public void ActualizarBalas(int cantidad)
    {
        balasTexto.text = "x" + cantidad.ToString();
    }

    // Actualizar el contador de vidas en la UI
    public void ActualizarVidas(int vidasRestantes)
    {
        for (int i = 0; i < vidasImagenes.Length; i++)
        {
            vidasImagenes[i].enabled = i < vidasRestantes;
        }
    }

    // Métodos para la transición de pantallas
    public void ShowResultados()
    {
        EnemigosControl.Instance.Gotoendgame();
    }

    public void Gotomenu()
    {
        EnemigosControl.Instance.EndMiniGame();
    }
}
