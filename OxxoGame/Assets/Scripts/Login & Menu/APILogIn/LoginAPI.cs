using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections;

public class LoginAPI : MonoBehaviour
{
    public static int? UserId { get; private set; }
    
    void Start()
    {
        // Verificar si existe un usuario_skinController en la escena
        if (usuario_skinController.Instancia == null)
        {
            // Si no existe, crear uno
            GameObject controllerObject = new GameObject("SkinController");
            controllerObject.AddComponent<usuario_skinController>();
        }
    }

    // Método para iniciar sesión
    public IEnumerator Login(string email, string password)
    {
        // Construir la URL con los parámetros
        string baseUrl = "https://localhost:7119/Login";
        string url = $"{baseUrl}?user={UnityWebRequest.EscapeURL(email)}&contrasena={UnityWebRequest.EscapeURL(password)}";

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.certificateHandler = new ForceAcceptAll();
            // Enviar la solicitud
            yield return request.SendWebRequest();

            // Manejar la respuesta
            if (request.result == UnityWebRequest.Result.Success)
            {
                if (int.TryParse(request.downloadHandler.text, out int userId))
                {
                    UserId = userId; // Guardar el ID del usuario
                    Debug.Log($"Login successful. User ID: {UserId}");
                    
                    // Verificar que la instancia existe antes de llamar al método
                    if (usuario_skinController.Instancia != null)
                    {
                        StartCoroutine(usuario_skinController.Instancia.getSkinActive(UserId));
                    }
                    else
                    {
                        Debug.Log("usuario_skinController.Instancia is null!");
                    }
                }
                else
                {
                    Debug.Log("Failed to parse user ID.");
                }
            }
            else
            {
                Debug.Log($"Login failed: {request.error}");
            }
        }
    }
}