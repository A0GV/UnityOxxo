using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    public int currentExperience = 0; // Experiencia actual del jugador
    private UIControlEnemigos uiController;

    void Start()
    {
        uiController = FindFirstObjectByType<UIControlEnemigos>();
    }

    public void AddExperience(int amount)
    {
        currentExperience += amount;
        Debug.Log($"Experiencia añadida: {amount}. Experiencia total: {currentExperience}");

        if (uiController != null)
        {
            uiController.ActualizarExperiencia(currentExperience);
        }
    }
}
