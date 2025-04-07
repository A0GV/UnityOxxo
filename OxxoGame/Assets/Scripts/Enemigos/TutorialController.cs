using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{
    public static bool saltarTutorial = false; // Variable estática para recordar si se debe saltar el tutorial

    public GameObject[] panelesPorPaso;  // Todos los paneles del tutorial en orden
    private int currentStep = 0;

    void Awake()
    {
        // Desactivar todos los paneles al inicio
        foreach (GameObject panel in panelesPorPaso)
        {
            if (panel != null)
                panel.SetActive(false);
        }
    }

    void Start()
    {
        if (saltarTutorial)
        {
            CargarEscenaJuego();
        }
        else
        {
            if (panelesPorPaso.Length > 0 && panelesPorPaso[0] != null)
            {
                currentStep = 0;
                MostrarPaso(currentStep);
            }
            else
            {
                Debug.LogWarning("El primer panel del tutorial no está asignado o el array está vacío.");
            }
        }
    }

    public void SiguientePaso()
    {
        if (currentStep >= panelesPorPaso.Length - 1)
        {
            // Si es el último paso, cargar la escena del juego
            CargarEscenaJuego();
            return;
        }

        if (panelesPorPaso[currentStep] != null)
            panelesPorPaso[currentStep].SetActive(false);

        currentStep++;

        if (currentStep < panelesPorPaso.Length)
        {
            MostrarPaso(currentStep);
        }
    }

    public void SaltarTutorial()
    {
        saltarTutorial = true;
        CargarEscenaJuego();
    }

    public void SalirJuego()
    {
        Debug.Log("Saliendo del juego...");
        UnityEditor.EditorApplication.isPlaying = false;
        // Application.Quit();
    }
    void MostrarPaso(int index)
    {
        if (index >= 0 && index < panelesPorPaso.Length)
        {
            GameObject panel = panelesPorPaso[index];
            if (panel == null)
            {
                Debug.LogError($"El panel en el índice {index} es null. Verifica el array panelesPorPaso en el Inspector.");
                return;
            }

            panel.SetActive(true);

            // Centrar el panel en la pantalla
            RectTransform rt = panel.GetComponent<RectTransform>();
            if (rt != null)
            {
                rt.anchoredPosition = Vector2.zero;
            }
        }
        else
        {
            Debug.LogError("Índice fuera de rango al intentar mostrar un panel.");
        }
    }

    void CargarEscenaJuego()
    {
        SceneManager.LoadScene("EnemigosGameScene");
    }
}