using UnityEngine;

public class DDButtonSolve3 : MonoBehaviour
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
        return;
    }
}

    public void Resolver()
    {
        resolver.SolveProblem(problema);
    }
}
