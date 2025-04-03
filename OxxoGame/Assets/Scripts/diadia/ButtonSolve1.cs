using UnityEngine;

public class ButtonSolve1 : MonoBehaviour
{
    // Instancias 
    public DDResolver resolver;
    private Problema problema;

    // Sets problem when clicked 
    public void SetProblema(Problema p)
    {
        problema = p;
    }

    public void Resolver()
    {
        resolver.SolveProblem(problema);
    }
}
