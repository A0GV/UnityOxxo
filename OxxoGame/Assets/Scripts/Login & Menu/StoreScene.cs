using UnityEngine;
using UnityEngine.SceneManagement;

public class StoreScene : MonoBehaviour
{
    public GameObject comprar;
    public GameObject articulo;
    public GameObject tienda;


    public void GoBackToMenu(){
        SceneManager.LoadScene("MenuScene");
    }

    public void ShowBalance(){
        comprar.SetActive(true);
        articulo.SetActive(false);
    }

    public void ShowStore(){
        tienda.SetActive(true);
        articulo.SetActive(false);
    }

    public void ShowItem(){
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
