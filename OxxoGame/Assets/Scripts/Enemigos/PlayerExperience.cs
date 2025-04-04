using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    public int currentExperience = 0; // Experiencia actual del jugador

    public void AddExperience(int amount)
    {
        currentExperience += amount;
        Debug.Log($"Experiencia añadida: {amount}. Experiencia total: {currentExperience}");
    }
}
