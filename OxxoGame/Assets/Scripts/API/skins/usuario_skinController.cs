
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Newtonsoft.Json;

public class usuario_skinController : MonoBehaviour
{
    public static usuario_skinController Instancia;

    private void Awake()
    {
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instancia != this)
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator getSkinActive(int? userId)
    {
        if (!userId.HasValue)
        {
            Debug.LogError("User ID is null. Cannot get skin.");
            yield break;
        }

        string JSONurl = $"https://localhost:7119/Login/datosSkin?userId={userId.Value}";
        Debug.Log($"Fetching skin data from: {JSONurl}");

        UnityWebRequest web = UnityWebRequest.Get(JSONurl);
        web.certificateHandler = new ForceAcceptAll();
        yield return web.SendWebRequest();

        if (web.result == UnityWebRequest.Result.Success)
        {
            try
            {
                var dataofNPC = JsonConvert.DeserializeObject<usuario_skin>(web.downloadHandler.text);
                PlayerPrefs.SetInt("id_skin", dataofNPC.id_skin);
                PlayerPrefs.Save();
                Debug.Log($"Skin ID {dataofNPC.id_skin} saved successfully");
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Error deserializing skin data: {ex.Message}");
            }
        }
        else
        {
            Debug.LogError($"Failed to get skin: {web.error}");
        }
    }

    
}
