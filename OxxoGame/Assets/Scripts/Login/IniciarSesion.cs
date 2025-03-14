using UnityEngine;
using UnityEngine.SceneManagement; 

public class IniciarSesion : MonoBehaviour
{
    // Used to change to game scene if user starts their game
    public void PulsarIniciar()
    {
        SceneManager.LoadScene("MenuScene");
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
