using UnityEngine;

public class Bala : MonoBehaviour
{
    public float velocidad = 10f;

    void Update()
    {
        // Mueve la bala hacia adelante
        transform.Translate(Vector3.up * velocidad * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        // Destruye la bala si sale de la pantalla
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Destruir al chocar con un enemigo
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}

