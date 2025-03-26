using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public GameObject[] panelesPorPaso;  // Todos los paneles del tutorial en orden
    public GameObject panelJuego;        // Panel del gameplay final

    private int currentStep = 0;

    void Start()
    {
        MostrarPaso(0); // Comienza con el primer panel activo
    }

    public void SiguientePaso()
    {
        // Oculta el panel actual
        panelesPorPaso[currentStep].SetActive(false);
        currentStep++;

        // Si hay más pasos, muestra el siguiente
        if (currentStep < panelesPorPaso.Length)
        {
            MostrarPaso(currentStep);
        }
        else
        {
            IniciarJuego();
        }
    }

    void MostrarPaso(int index)
    {
        panelesPorPaso[index].SetActive(true);
    }

    void IniciarJuego()
    {
        // Oculta todo el tutorial
        gameObject.SetActive(false);

        // Activa el panel del gameplay
        panelJuego.SetActive(true);
    }
}
