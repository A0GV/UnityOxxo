using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public void GoBackToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void ShowBalance()
    {
        // Obtener el saldo actual (no sobreescribir cada vez)
        int saldoActual = PlayerPrefs.HasKey("Elotes") ? PlayerPrefs.GetInt("Elotes") : 1500;
        
        if (!PlayerPrefs.HasKey("Elotes")) {
            PlayerPrefs.SetInt("Elotes", 1500);
        }
        
        if (dinero <= saldoActual) // Si tienes suficiente dinero
        {
            // Restar el costo
            saldoActual -= dinero;
            PlayerPrefs.SetInt("Elotes", saldoActual);
            
            // Guardar el ID de skin correctamente (SIN sumar 1)
            PlayerPrefs.SetInt("id_skin", idItem);
            PlayerPrefs.Save();
            
            Debug.Log($"Skin comprada: {idItem}, saldo restante: {saldoActual}");
            
            comprar.SetActive(true);
            articulo.SetActive(false);
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
        // Asigna el valor al campo de la clase, no reasignes el parámetro
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
            if (i+1 == currentSkinId)
            {
                Debug.Log($"El sombrero {i+1} ha sido equipado - mostrando botón de quitar");
                Adquiridos[i].gameObject.SetActive(true);
            }
            else
            {
                // Asegúrate de que los botones de las otras skins estén ocultos
                Adquiridos[i].gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
