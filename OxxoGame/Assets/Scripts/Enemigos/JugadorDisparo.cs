using UnityEngine;

public class JugadorDisparo : MonoBehaviour
{
    public GameObject balaPrefab;
    public Transform firePoint;
    public Transform arma;  // Referencia al objeto de arma
    public int maxBalas = 5;
    private int balasActuales;

    void Start()
    {
        balasActuales = maxBalas; // Inicializar con el máximo de balas
    }

    void Update()
    {
        Apuntar();
        
        if (Input.GetMouseButtonDown(0) && balasActuales > 0)
        {
            Disparar();
        }
    }
    void Apuntar()
    {
        // Obtener la posición del mouse en el mundo
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direccion = mousePos - transform.position;
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg - 90f; // Ajuste para que apunte hacia arriba

        // Rotar el arma hacia el cursor
        if (arma != null)
        {
            arma.rotation = Quaternion.Euler(0, 0, angulo);
        }

        if (firePoint != null)
        {
            firePoint.rotation = Quaternion.Euler(0, 0, angulo);
        }
    }

    void Disparar()
    {
        // Instanciar la bala en el firePoint con la rotación actual
        Instantiate(balaPrefab, firePoint.position, firePoint.rotation);
        balasActuales--;
        Debug.Log("Balas restantes: " + balasActuales);
    }

    public void Recargar(int cantidad)
    {
        balasActuales = Mathf.Min(maxBalas, balasActuales + cantidad);
        Debug.Log("Balas recargadas: " + balasActuales);
    }
}

