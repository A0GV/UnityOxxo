using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float speed = 2.0f;  // Velocidad del enemigo
    public int experiencePoints = 50; // Experiencia que otorga este enemigo

    private Transform player;   // Referencia al jugador
    private Rigidbody2D rb;     // Referencia al Rigidbody2D del enemigo

    private Vector2 moveDirection; // Dirección de movimiento del enemigo

    void Start()
    {
        // Encontrar al jugador en la escena
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Obtener el Rigidbody2D del enemigo
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player != null)
        {
            // Calcular la dirección hacia el jugador en el plano 2D
            Vector2 posicionJugador = new Vector2(player.position.x, player.position.y);
            Vector2 posicionEnemigo = new Vector2(transform.position.x, transform.position.y);
            moveDirection = (posicionJugador - posicionEnemigo).normalized;
        }
    }

    void FixedUpdate()
    {
        if (rb != null)
        {
            // Aplicar la velocidad al Rigidbody2D
            rb.linearVelocity = moveDirection * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            // Agregar experiencia al jugador
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                PlayerExperience playerExperience = playerObject.GetComponent<PlayerExperience>();
                if (playerExperience != null)
                {
                    playerExperience.AddExperience(experiencePoints);
                }
            }

            // Destruir tanto la bala como el enemigo al colisionar
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Player"))
        {
            // Reproducir el sonido de impacto con el jugador
            if (EnemigosSFX.Instance != null)
            {
                EnemigosSFX.Instance.PlayEnemyHitPlayer();
            }

            // Destruir el enemigo si toca al jugador
            Destroy(gameObject);

            // Llamar a un método en el jugador para reducir la vida
            collision.GetComponent<PlayerHealth>()?.TakeDamage(1);
        }
    }
}
