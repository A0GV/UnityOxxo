using UnityEngine;
using UnityEngine.EventSystems;

public class JugadorDisparo : MonoBehaviour
{
    public GameObject balaPrefab; // Prefab de la bala que se disparará
    public Transform firePoint;   // Punto desde donde se disparan las balas
    public Transform arma;        // Referencia al objeto de arma
    public int maxBalas = 5;      // Máximo número de balas que puede tener el jugador
    private int balasActuales;    // Número actual de balas disponibles
    private UIControlEnemigos uiController; // Controlador de la UI para actualizar las balas
    public GameObject panelJuego; // Referencia al PanelJuego

    void Start()
    {
        // Inicializar las balas al máximo al inicio del juego
        balasActuales = 0;

        // Buscar el controlador de UI en la escena
        uiController = Object.FindFirstObjectByType<UIControlEnemigos>();

        // Actualizar la UI con el número inicial de balas
        if (uiController != null)
        {
            uiController.ActualizarBalas(balasActuales);
        }
        else
        {
            Debug.LogWarning("UIControlEnemigos no encontrado en la escena.");
        }
    }

    void Update()
    {
        // Apuntar el arma hacia el cursor del mouse
        Apuntar();

        // Disparar si se presiona el botón izquierdo del mouse, el juego no está pausado y no se hizo clic en la UI
        if (Input.GetMouseButtonDown(0) && balasActuales > 0 && Time.timeScale > 0 && !EventSystem.current.IsPointerOverGameObject())
        {
            // Verificar si el clic no ocurre sobre PanelJuego
            if (panelJuego != null && RectTransformUtility.RectangleContainsScreenPoint(panelJuego.GetComponent<RectTransform>(), Input.mousePosition, Camera.main))
            {
                Debug.Log("Clic en PanelJuego, no se dispara.");
                return;
            }

            Disparar();
        }
    }

    void Apuntar()
    {
        // Obtener la posición del mouse en el mundo
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direccion = mousePos - transform.position;

        // Calcular el ángulo hacia el cursor
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg - 90f; // Ajuste para que apunte hacia arriba

        // Rotar el arma hacia el cursor
        if (arma != null)
        {
            arma.rotation = Quaternion.Euler(0, 0, angulo);
        }

        // Rotar el punto de disparo hacia el cursor
        if (firePoint != null)
        {
            firePoint.rotation = Quaternion.Euler(0, 0, angulo);
        }
    }

    void Disparar()
    {
        // Instanciar la bala en el firePoint con la rotación actual
        Instantiate(balaPrefab, firePoint.position, firePoint.rotation);

        // Reducir el número de balas disponibles
        balasActuales--;

        // Actualizar la UI con el nuevo número de balas
        if (uiController != null)
        {
            uiController.ActualizarBalas(balasActuales);
        }

        // Mostrar en la consola el número de balas restantes
        Debug.Log("Balas restantes: " + balasActuales);
    }

    public void Recargar(int cantidad)
    {
        // Recargar balas hasta el máximo permitido
        balasActuales = Mathf.Min(maxBalas, balasActuales + cantidad);

        // Actualizar la UI con el nuevo número de balas
        if (uiController != null)
        {
            uiController.ActualizarBalas(balasActuales);
        }

        // Mostrar en la consola el número de balas recargadas
        Debug.Log("Balas recargadas: " + balasActuales);
    }
}

