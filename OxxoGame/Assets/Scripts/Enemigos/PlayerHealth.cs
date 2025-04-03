using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;

    void Start()
    {
        currentLives = maxLives;
    }

    public void TakeDamage(int damage)
    {
        currentLives -= damage;
        Debug.Log("Vidas restantes: " + currentLives);

        if (currentLives <= 0)
        {
            Debug.Log("¡El jugador ha muerto!");
            // Llamar a un método que muestre el panel de Game Over
        }
    }
}
