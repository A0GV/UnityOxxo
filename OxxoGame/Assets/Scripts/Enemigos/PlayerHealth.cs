using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;
    private UIControlEnemigos uiController;

    void Start()
    {
        currentLives = maxLives;
        uiController = FindFirstObjectByType<UIControlEnemigos>();
        uiController.ActualizarVidas(currentLives);
    }

    public void TakeDamage(int damage)
    {
        currentLives -= damage;
        uiController.ActualizarVidas(currentLives);

        if (currentLives <= 0)
        {
            Debug.Log("¡El jugador ha muerto!");
            uiController.ShowPanel("Perder");
        }
    }
}
