using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public APIEnemigos apiInstance; 

    public EnemigosSFX enemigosSFX; // Referencia al script de sonidos de enemigos

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
        // Reproducir el sonido de impacto con el jugador
        if (enemigosSFX != null)
        {
            enemigosSFX.PlayEnemyHitPlayer();
        }
        currentLives -= damage;
        uiController.ActualizarVidas(currentLives);

        if (currentLives <= 0)
        {
            Debug.Log("¡El jugador ha muerto!");
            uiController.ShowPanel("Finalizar");
            StartCoroutine(apiInstance.PostDatosJuego());
            Time.timeScale = 0; // Pausar el juego al llegar al panel de finalizar
        }
    }
}
