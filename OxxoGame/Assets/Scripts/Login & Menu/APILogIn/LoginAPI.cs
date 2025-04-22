using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections;

public class LoginAPI : MonoBehaviour
{
    // Singleton instance
    public static LoginAPI Instance { get; private set; }
    
    // User ID stored statically
    public static int? UserId { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject); 
        
        Debug.Log("LoginAPI initialized as singleton");
    }

    // Método para iniciar sesión
    public IEnumerator Login(string email, string password)
    {
        // Construir la URL con los parámetros
        string baseUrl = "https://localhost:7119/Login";
        string url = $"{baseUrl}?user={UnityWebRequest.EscapeURL(email)}&contrasena={UnityWebRequest.EscapeURL(password)}";
        Debug.Log($"Attempting login with URL: {url}");

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.certificateHandler = new ForceAcceptAll();
            request.timeout = 10; // Set timeout to 10 seconds
            
            // Enviar la solicitud
            yield return request.SendWebRequest();

            // Manejar la respuesta
            if (request.result == UnityWebRequest.Result.Success)
            {
                if (int.TryParse(request.downloadHandler.text, out int userId))
                {
                    UserId = userId; // Guardar el ID del usuario
                    Debug.Log($"Login successful. User ID: {UserId}");
                    
                    // Store skin data immediately after login
                    yield return GetSkinActiveLocal(UserId);
                }
                else
                {
                    Debug.LogError("Failed to parse user ID from response: " + request.downloadHandler.text);
                }
            }
            else
            {
                Debug.LogError($"Login failed: {request.error}, Response Code: {request.responseCode}");
                if (request.downloadHandler != null)
                {
                    Debug.LogError("Response body: " + request.downloadHandler.text);
                }
            }
        }
    }

    public IEnumerator GetSkinActiveLocal(int? userId)
    {
        if (!userId.HasValue)
        {
            Debug.LogError("User ID is null. Cannot get skin.");
            yield break;
        }

        string JSONurl = $"https://localhost:7119/Login/datosSkin?userId={userId.Value}";
        Debug.Log($"Fetching skin data from: {JSONurl}");
        // skin default por si falla
        PlayerPrefs.SetInt("id_skin", 0); // Default skin ID
        PlayerPrefs.Save();

        using (UnityWebRequest web = UnityWebRequest.Get(JSONurl))
        {
            web.certificateHandler = new ForceAcceptAll();
            web.timeout = 10; // Reduced timeout to be more responsive
            
            Debug.Log("Web request started - pre yield return");
            
            // Simple approach - just use the built-in Unity coroutine system
            yield return web.SendWebRequest();
            
            Debug.Log($"Web request completed with result: {web.result}");
            
            if (web.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Success getting skin data");
                Debug.Log($"Response data: {web.downloadHandler.text}");
                
                try
                {
                    var dataofNPC = JsonConvert.DeserializeObject<usuario_skin>(web.downloadHandler.text);
                    PlayerPrefs.SetInt("id_skin", dataofNPC.id_skin-1);
                    PlayerPrefs.Save();
                    Debug.Log($"Skin ID {dataofNPC.id_skin} saved successfully to PlayerPrefs");
                }
                catch (System.Exception ex)
                {
                    Debug.LogError($"Error deserializing skin data: {ex.Message}");
                    Debug.LogError($"Raw response: {web.downloadHandler.text}");
                }
            }
            else
            {
                Debug.LogError($"Failed to get skin. Result: {web.result}, Error: {web.error}");
                Debug.LogError($"Response code: {web.responseCode}");
                
                // Try to log the response body even on error
                if (web.downloadHandler != null && !string.IsNullOrEmpty(web.downloadHandler.text))
                {
                    Debug.LogError($"Error response body: {web.downloadHandler.text}");
                }
            }
        }
    }

    // Add a public method to retrieve skin data manually if needed
    public void FetchSkinData()
    {
        if (UserId.HasValue)
        {
            StartCoroutine(GetSkinActiveLocal(UserId));
        }
        else
        {
            Debug.LogError("Cannot fetch skin data: User is not logged in");
        }
    }
}
