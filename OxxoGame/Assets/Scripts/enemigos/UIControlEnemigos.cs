using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIControlEnemigos : MonoBehaviour
{

    public APIEnemigos apiInstance; 

    // Paneles de UI
    public GameObject Estatus;
    public GameObject Pregunta;
    public GameObject RespuestaCorrecta;
    public GameObject RespuestaIncorrecta;
    public GameObject Pausa;
    public GameObject Finalizar; 

    // Elementos de UI para balas, vidas, experiencia y maíces
    public Text balasTexto;  // Texto que muestra la cantidad de balas
    public Text experienciaPausaTexto; // Texto que muestra la experiencia en el panel de pausa
    public Text experienciaFinalizarTexto; // Texto que muestra la experiencia en el panel de finalizar
    public Text maicesPausaTexto; // Texto para mostrar maíces en el panel de pausa
    public Text maicesFinalizarTexto; // Texto para mostrar maíces en el panel de finalización
    public Image[] vidasImagenes;  // Array de imágenes de corazones

    // Botones de los paneles
    public Button botonPausa;
    public Button botonContinuar;
    public Button botonFinalizar;
    public Button botonNuevaPartida;
    public Button botonRegresarMenu;

    private bool juegoPausado = false;

    void Start()
    {
        // Asignar funciones a los botones
        if (botonPausa != null) botonPausa.onClick.AddListener(TogglePausa);
        if (botonContinuar != null) botonContinuar.onClick.AddListener(ContinuarJuego);
        if (botonFinalizar != null) botonFinalizar.onClick.AddListener(AbrirFinalizar);
        if (botonNuevaPartida != null) botonNuevaPartida.onClick.AddListener(NuevaPartida);
        if (botonRegresarMenu != null) botonRegresarMenu.onClick.AddListener(RegresarMenu);

        // Asegurar que el juego comience sin estar pausado
        Time.timeScale = 1;
        juegoPausado = false;
    }

// Método para mostrar un panel específico
    public void ShowPanel(string panelName)
    {
        // Ocultar todos los paneles primero
        Estatus.SetActive(false);
        Pregunta.SetActive(false);
        RespuestaCorrecta.SetActive(false);
        RespuestaIncorrecta.SetActive(false);
        Pausa.SetActive(false);
        if (Finalizar != null) Finalizar.SetActive(false);

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
            case "Pausa":
                Pausa.SetActive(true);
                break;
            case "Finalizar":
                if (Finalizar != null) Finalizar.SetActive(true);
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

// Actualizar la experiencia en la UI
    public void ActualizarExperiencia(int experiencia)
    {
        if (experienciaPausaTexto != null)
            experienciaPausaTexto.text = "x" + experiencia;

        if (experienciaFinalizarTexto != null)
            experienciaFinalizarTexto.text = "x" + experiencia;
    }

    // Nuevo método para actualizar los maíces
    public void ActualizarMaices(int maices)
    {
        if (maicesPausaTexto != null)
            maicesPausaTexto.text = "x" + maices;

        if (maicesFinalizarTexto != null)
            maicesFinalizarTexto.text = "x" + maices;
    }

// Métodos para pausar y continuar el juego
    void TogglePausa()
    {
        if (juegoPausado)
        {
            ContinuarJuego();
        }
        else
        {
            PausarJuego();
        }
    }

    void PausarJuego()
    {
        juegoPausado = true;
        Time.timeScale = 0;
        Pausa.SetActive(true);
        Estatus.SetActive(true);
    }

    void ContinuarJuego()
    {
        juegoPausado = false;
        Time.timeScale = 1;
        Pausa.SetActive(false);
    }

    void AbrirFinalizar()
    {
        Pausa.SetActive(false);
        if (Finalizar != null) Finalizar.SetActive(true);
        StartCoroutine(apiInstance.PostDatosJuego());
        Time.timeScale = 0;
    }

    void NuevaPartida()
    {
        TutorialController.saltarTutorial = true;
        SceneManager.LoadScene("EnemigosGameScene");
    }

    void RegresarMenu()
    {
        Debug.Log("Saliendo al menú...");
        Time.timeScale = 1; // Asegurarse de que el tiempo esté en escala normal
        TutorialController.saltarTutorial = false; // Reinicia el valor al cargar el menú
        SceneManager.LoadScene("MenuScene");
    }
}
