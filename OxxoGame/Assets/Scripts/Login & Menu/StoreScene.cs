using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using UnityEngine.UI;

public class StoreScene : MonoBehaviour
{
    public GameObject comprar;
    public GameObject articulo;
    public GameObject tienda;
    public UnityEngine.UI.Image hat; //lo tuve que poner asi porque me daba erro pq algo vscode se llama igual
    public Text precio;
    public int idItem;
    public int dinero;
    public Sprite[] spSombreros;
    public Button[] Adquiridos;
    public Button buy;
    public Text restante;
    public Text saldoActual;

    void Awake()
    {
        if (LogicForBuy.elotesstore == 0)
        {
            saldoActual.text = PlayerPrefs.GetString("elotesforstore");
        }
        else
        {

            saldoActual.text = LogicForBuy.elotesstore.ToString();
        }
    }
    public void GoBackToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void ShowBalance()
    {
        // Obtener el saldo actual
        int saldoActual = LogicForBuy.elotesstore;


        if (dinero <= saldoActual) // Si tienes suficiente dinero
        {
            // Restar el costo
            saldoActual -= dinero;
            StartCoroutine(Updatemoney(dinero * -1));

            PlayerPrefs.SetInt("Elotes", saldoActual);

            PlayerPrefs.SetInt("id_skin", idItem);
            PlayerPrefs.Save();

            restante.text = saldoActual.ToString();
            Debug.Log($"Skin comprada: {idItem}, saldo restante: {saldoActual}");

            comprar.SetActive(true);
            articulo.SetActive(false);
            StartCoroutine(FirstBuy(idItem));
        }
        else
        {
            Debug.Log("No tienes suficientes elotes para comprar esta skin");
            ShowStore();
        }
    }

    public void ShowStore()
    {
        tienda.SetActive(true);
        articulo.SetActive(false);
    }

    public void ShowItem(int itemId)
    {
        this.idItem = itemId;

        switch (itemId) // Usa el parámetro para el switch
        {
            case 1:
                dinero = 75;
                precio.text = "COMPRAR - 75";
                hat.sprite = spSombreros[0];
                Debug.Log(this.idItem);
                break;
            case 2:
                dinero = 65;
                precio.text = "COMPRAR - 65";
                hat.sprite = spSombreros[1];
                Debug.Log(this.idItem);
                break;
            case 3:
                dinero = 50;
                precio.text = "COMPRAR - 50";
                hat.sprite = spSombreros[2];
                Debug.Log(this.idItem);
                break;
            case 4:
                dinero = 50;
                precio.text = "COMPRAR - 50";
                hat.sprite = spSombreros[3];
                Debug.Log(this.idItem);
                break;
            default:
                break;
        }
        articulo.SetActive(true);
        comprar.SetActive(false);
    }

    public void pelonPelonete()
    {
        // Set skin ID to 0 (no skin/hat equipped)
        PlayerPrefs.SetInt("id_skin", 0);
        PlayerPrefs.Save();


        // Return to store view
        ShowStore();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Obtener la skin actual desde PlayerPrefs
        int currentSkinId = PlayerPrefs.HasKey("id_skin") ? PlayerPrefs.GetInt("id_skin") : 0;

        // Actualizar el campo idItem con el valor correcto
        this.idItem = currentSkinId;

        // Actualizar los botones "Quitar" basados en la skin actual
        for (int i = 0; i < Adquiridos.Length; i++)
        {
            // Ajustamos la comparación: i+1 para convertir índice (0-based) a ID (1-based)
            if (i + 1 == currentSkinId)
            {
                Debug.Log($"El sombrero {i + 1} ha sido equipado - mostrando botón de quitar");
                Adquiridos[i].gameObject.SetActive(true);
            }
            else
            {
                // Asegúrate de que los botones de las otras skins estén ocultos
                Adquiridos[i].gameObject.SetActive(false);
            }
        }

    }

    IEnumerator Updatemoney(int monedas)
    {
        string url = "https://localhost:7119/manageCurrency/AgregarDatosJuego";

        // Use form data para agregar los datos al JSON
        WWWForm form = new WWWForm();
        form.AddField("id_usuario", LoginAPI.UserId.ToString());
        form.AddField("id_juego", 0);
        form.AddField("monedas", monedas);
        form.AddField("exp", 0);

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
    public IEnumerator FirstBuy(int skin)
    {
        string url = $"https://localhost:7119/Login/NewCompra?userId={LoginAPI.UserId}&id_skin={skin + 1}";

        UnityWebRequest request = UnityWebRequest.Post(url, LoginAPI.UserId.ToString(), idItem.ToString()); // Para hacer un post 
        request.certificateHandler = new ForceAcceptAll(); // Para accept all de integradora
        yield return request.SendWebRequest();

    }
    public IEnumerator UpdateSkin(int Skin)
    {
        Debug.Log($"UpdateSkin called with skin ID: {Skin}");

        string url = "https://localhost:7119/Login/Actualizaractivo";

        // Create JSON payload - ensure this is the exact ID expected by the server
        string jsonPayload = $"{{\"userId\":{LoginAPI.UserId},\"id_skin\":{Skin + 1}}}";
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonPayload);

        Debug.Log($"Sending JSON: {jsonPayload}");

        // Manually create the UnityWebRequest for PUT
        UnityWebRequest request = new UnityWebRequest(url, "PUT");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.certificateHandler = new ForceAcceptAll();

        // Send request
        yield return request.SendWebRequest();
        Debug.Log($"Actualizando skin: {Skin}");

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Error en la solicitud: {request.error}");
            Debug.LogError($"Código HTTP: {request.responseCode}");
            Debug.LogError($"Respuesta: {request.downloadHandler.text}");
        }
        else
        {
            Debug.Log("Solicitud exitosa");
            Debug.Log($"Respuesta: {request.downloadHandler.text}");

            // Save the skin selection locally - make sure this ID matches what your UI expects
            PlayerPrefs.SetInt("id_skin", Skin);
            PlayerPrefs.Save();
            GoBackToMenu();
        }
    }

    public void changeSkin(int skin)
    {
        // Add debug logging to track the skin ID
        Debug.Log($"changeSkin called with parameter: {skin}");
        StartCoroutine(UpdateSkin(skin));
    }




    // Update is called once per frame
    void Update()
    {

    }
}
