using UnityEngine;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(fileName = "ProblemaNuevo", menuName = "DiaDia/Problemas")]
public class Problema : ScriptableObject
{
    // Variables 
    public string problemaNombre;
    public int impactoNegativo; // Negative impact of each problem
    public string descripcion; // Describes problem to fix
    public Sprite icon; // A ver si ayuda con el !

    public Vector2 posicion;

    public bool renderedIcon = false; // Cuando crea, default false rendered

    // Functions 
    // To lower the amount of money that is earned in a day
    public int GetImpactoNegativo() 
    {
        return impactoNegativo; 
    }

    // To get the name of problem
    public string GetNombreProblema()
    {
        return problemaNombre;
    }

    // To get the description of problem
    public string GetDescripcionProblema() 
    {
        return descripcion;
    }

    // Gets status whether rendered or not
    public bool GetRenderStatus()
    {
        return renderedIcon; 
    }

    // Para cambiar status si está rendered o no
    public void SetRenderStatus(bool status)
    {
        renderedIcon = status;
    }
}

