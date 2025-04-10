using UnityEngine;
using UnityEngine.Networking; 
using Newtonsoft.Json; 
using System.Collections.Generic;
using System.Collections;

public class APIEnemigos : MonoBehaviour
{


    public int idUsuario = 7; // ID del usuario, puedes asignarlo desde el Inspector o dinámicamente
    private PreguntaManager preguntaManager;
    private PlayerExperience playerExperience;

    void Start()
    {
        // Obtener referencias a los scripts necesarios
        preguntaManager = FindFirstObjectByType<PreguntaManager>();
        playerExperience = FindFirstObjectByType<PlayerExperience>();

        if (preguntaManager == null || playerExperience == null)
        {
            Debug.LogError("No se encontraron los scripts PreguntaManager o PlayerExperience.");
        }
    }

    // Para post datos de jugada en historial y usuario_historial 
    public IEnumerator PostDatosJuego()
    {
        string url = "https://localhost:7119/manageCurrency/AgregarDatosJuego";

        // Validar que las referencias sean válidas
        if (preguntaManager == null || playerExperience == null)
        {
            Debug.LogError("No se pueden enviar los datos porque falta una referencia.");
            yield break;
        }

        // Obtener los datos necesarios
        int monedas = preguntaManager.maicesCorrectos;
        int exp = playerExperience.currentExperience;        

        // Use form data para agregar los datos al JSON
        WWWForm form = new WWWForm();
        form.AddField("id_usuario", idUsuario);
        form.AddField("id_juego", 2);
        form.AddField("monedas", monedas);
        form.AddField("exp", exp);

        UnityWebRequest request = UnityWebRequest.Post(url, form); // Para hacer un post 
        request.certificateHandler = new ForceAcceptAll(); // Para accept all de integradora
        yield return request.SendWebRequest();

        // Ver si funciona
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error al enviar datos: " + request.error);
        }
        else
        {
            Debug.Log("Se agregó todo :D");
        }
    }

    public IEnumerator CargarPreguntasDesdeAPI(System.Action<List<PreguntaManager.Pregunta>> callback)
    {
        string apiUrl = "https://localhost:7119/Pregunta/GetPreguntasEnemigo"; // URL de la API
        
        using (UnityWebRequest request = UnityWebRequest.Get(apiUrl))
        {
            request.certificateHandler = new ForceAcceptAll(); // Aceptar todos los certificados SSL
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string json = request.downloadHandler.text;
                Debug.Log("Respuesta de la API: " + json);

                try
                {
                    // Deserializar el JSON en una lista de preguntas
                    List<PreguntaManager.Pregunta> preguntas = JsonConvert.DeserializeObject<List<PreguntaManager.Pregunta>>(json);

                    if (preguntas == null || preguntas.Count == 0)
                    {
                        Debug.LogError("La API no devolvió preguntas válidas.");
                        callback(null);
                        yield break;
                    }

                    Debug.Log("Preguntas cargadas correctamente desde la API.");
                    callback(preguntas);
                }
                catch (JsonException ex)
                {
                    Debug.LogError("Error al deserializar el JSON: " + ex.Message);
                    callback(null);
                }
            }
            else
            {
                Debug.LogError("Error al cargar preguntas desde la API: " + request.error);
                callback(null);
            }
        }
    }
}