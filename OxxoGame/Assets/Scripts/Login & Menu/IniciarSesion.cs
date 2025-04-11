using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Para manejar los campos de entrada
using System.Collections;

public class IniciarSesion : MonoBehaviour
{
    [SerializeField] private InputField correoInput; // Campo de entrada para el correo
    [SerializeField] private InputField contrasenaInput; // Campo de entrada para la contraseña
    [SerializeField] private GameObject panelError; // Panel para mostrar errores (opcional)
    [SerializeField] private LoginAPI loginAPI; // Asignar manualmente desde el Inspector

    void Start()
    {
        if (loginAPI == null)
        {
            loginAPI = FindAnyObjectByType<LoginAPI>(); // Buscar el componente en toda la escena
            if (loginAPI == null)
            {
                Debug.Log("El componente LoginAPI no está presente en la escena.");
            }
        }

        if (correoInput == null || contrasenaInput == null /* || panelError == null */)
        {
            Debug.Log("Uno o más campos de entrada no están asignados en el Inspector.");
        }
    }

    public void PulsarIniciar()
    {
        string correo = correoInput.text;
        string contrasena = contrasenaInput.text;

        // Validar que los campos no estén vacíos
        if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contrasena))
        {
            MostrarPanelError("Por favor, completa todos los campos."); // Comentado por ahora
            Debug.Log("Por favor, completa todos los campos.");
            return;
        }

        // Iniciar la corrutina para autenticar al usuario
        StartCoroutine(IniciarSesionCoroutine(correo, contrasena));
    }

    private IEnumerator IniciarSesionCoroutine(string correo, string contrasena)
    {
        yield return StartCoroutine(loginAPI.Login(correo, contrasena));

        if (LoginAPI.UserId.HasValue)
        {
            // Inicio de sesión exitoso
            Debug.Log($"Inicio de sesión exitoso. ID de usuario: {LoginAPI.UserId}");
            SceneManager.LoadScene("MenuScene"); // Cambiar a la escena del menú
        }
        else
        {
            // Inicio de sesión fallido
            MostrarPanelError("Credenciales inválidas. Intenta de nuevo."); // Comentado por ahora
            Debug.Log("Credenciales inválidas. Intenta de nuevo.");
        }
    }


    private void MostrarPanelError(string mensaje)
    {
        if (panelError != null)
        {
            // Aquí puedes configurar el panel para mostrar el mensaje de error
            panelError.SetActive(true);
            // Opcional: Si el panel tiene un texto hijo, puedes configurarlo aquí
            Text textoError = panelError.GetComponentInChildren<Text>();
            if (textoError != null) textoError.text = mensaje;
        }
        Debug.Log(mensaje);
    }

}
