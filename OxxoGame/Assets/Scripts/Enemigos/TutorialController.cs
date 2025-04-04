using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public GameObject[] panelesPorPaso;  // Todos los paneles del tutorial en orden
    public GameObject panelJuego;        // Panel del gameplay final
    public GameObject scenery;           // Objeto Scenery

    private int currentStep = 0;

    void Awake()
    {
        // Desactivar todo al inicio para asegurar que nada aparezca antes del tutorial
        if (scenery != null)
            scenery.SetActive(false);

        if (panelJuego != null)
            panelJuego.SetActive(false);

        foreach (GameObject panel in panelesPorPaso)
        {
            if (panel != null)
                panel.SetActive(false);
        }
    }

    void Start()
    {
        // Asegurarse de que el primer panel siempre esté activo
        if (panelesPorPaso.Length > 0 && panelesPorPaso[0] != null)
        {
            currentStep = 0;
            MostrarPaso(currentStep); // Muestra el primer panel del tutorial
        }
        else
        {
            Debug.LogWarning("El primer panel del tutorial no está asignado o el array está vacío.");
        }
    }

    public void SiguientePaso()
    {
        Debug.Log($"currentStep: {currentStep}, panelesPorPaso.Length: {panelesPorPaso.Length}");
        if (currentStep >= panelesPorPaso.Length)
        {
            IniciarJuego();
            return;
        }

        if (panelesPorPaso[currentStep] == null)
        {
            Debug.LogError($"El panel en el índice {currentStep} es null. Verifica el array panelesPorPaso.");
            return;
        }

        panelesPorPaso[currentStep].SetActive(false);
        currentStep++;

        if (currentStep < panelesPorPaso.Length)
        {
            MostrarPaso(currentStep);
        }
        else
        {
            IniciarJuego();
        }
    }

    public void SaltarTutorial()
    {
        foreach (GameObject panel in panelesPorPaso)
        {
            if (panel != null)
                panel.SetActive(false);
        }

        IniciarJuego();
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
                rt.anchoredPosition = Vector2.zero; // Centra el panel en la pantalla
            }
        }
        else
        {
            Debug.LogError("Índice fuera de rango al intentar mostrar un panel.");
        }
    }

    void IniciarJuego()
    {
        gameObject.SetActive(false);

        if (scenery != null)
            scenery.SetActive(true);

        if (panelJuego != null)
            panelJuego.SetActive(true);

        // Mostrar el panel "Apuntar" al iniciar el juego
        FindFirstObjectByType<UIControlEnemigos>().ShowPanel("Estatus");
    }
}