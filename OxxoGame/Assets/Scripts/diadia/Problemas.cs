using UnityEngine;

[CreateAssetMenu(fileName = "ProblemaNuevo", menuName = "DiaDia/Problemas")]
public class Problema : ScriptableObject
{
    public string problemaNombre;
    public int impactoBase; // Negative value, e.g. -3, -5
    public string description;
    public Sprite icon; // A ver si ayuda con el !
}
