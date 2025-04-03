using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float speed = 2.0f;  // Velocidad del enemigo
    private Transform player;   // Referencia al jugador
    private Rigidbody2D rb;     // Referencia al Rigidbody2D del enemigo

    void Start()
    {
        // Encontrar al jugador en la escena
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player == null)
        {
            Debug.LogError("No se encontró el jugador con el tag 'Player'");
        }

        // Obtener el Rigidbody2D del enemigo
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("No se encontró un Rigidbody2D en el enemigo");
        }
    }

    void FixedUpdate()
    {
        MoverHaciaJugador();
    }

    void MoverHaciaJugador()
    {
        if (player != null && rb != null)
        {
            // Obtener las posiciones actuales del jugador y del enemigo
            Vector3 posicionJugador = player.position;
            Vector3 posicionEnemigo = transform.position;

            // Mostrar las posiciones en la consola
            Debug.Log($"Posición del jugador: {posicionJugador}, Posición del enemigo: {posicionEnemigo}");

            // Calcular la dirección hacia el jugador en el plano 2D (ignorando el eje Z)
            Vector2 direccion = new Vector2(posicionJugador.x - posicionEnemigo.x, posicionJugador.y - posicionEnemigo.y).normalized;

            Debug.Log($"Dirección calculada: {direccion}");

            // Mover el enemigo hacia el jugador usando Rigidbody2D
            rb.linearVelocity = direccion * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            // Destruir tanto la bala como el enemigo al colisionar
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Player"))
        {
            // Destruir el enemigo si toca al jugador
            Destroy(gameObject);

            // Llamar a un método en el jugador para reducir la vida
            collision.GetComponent<PlayerHealth>()?.TakeDamage(1);
            Debug.Log("El enemigo tocó al jugador");
        }
    }
}
