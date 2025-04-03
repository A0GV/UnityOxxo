using UnityEngine;

public class DDButtonSolve2 : MonoBehaviour
{
    // Instancias 
    public DDResolver resolver;
    private Problema problema;

    // Sets problem when clicked 
    public void SetProblema(Problema p)
{
    problema = p;
    
    if (problema == null)
    {
        Debug.LogError($"SetProblema recibió un problema null en {gameObject.name}");
        return;
    }

    Debug.Log($"Asignado a botón: {problema.GetNombreProblema()}");
}

    public void Resolver()
    {
        resolver.SolveProblem(problema);
    }
}
