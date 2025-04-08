using UnityEngine;
using UnityEngine.Networking; 
using Newtonsoft.Json; 
using System.Collections.Generic;
using System.Collections;

public class LoginAPI : MonoBehaviour
{
    public static int? UserId { get; private set; }

    // Método para iniciar sesión
    public IEnumerator Login(string email, string password)
    {
        // Construir la URL con los parámetros
        string baseUrl = "https://localhost:7119/Login";
        string url = $"{baseUrl}?user={UnityWebRequest.EscapeURL(email)}&contrasena={UnityWebRequest.EscapeURL(password)}";

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            // Enviar la solicitud
            yield return request.SendWebRequest();

            // Manejar la respuesta
            if (request.result == UnityWebRequest.Result.Success)
            {
                if (int.TryParse(request.downloadHandler.text, out int userId))
                {
                    UserId = userId; // Guardar el ID del usuario
                    Debug.Log($"Login successful. User ID: {UserId}");
                }
                else
                {
                    Debug.LogError("Failed to parse user ID.");
                }
            }
            else
            {
                Debug.LogError($"Login failed: {request.error}");
            }
        }
    }
}