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
                Debug.Log(this.idItem); // Usa this.idItem para logging
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }


}
