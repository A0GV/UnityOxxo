using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScene : MonoBehaviour
{  
    // Sonido
    DDSFX sonidoBoton; 

    //Manda a la escena de juego seleccionada
    public void StartToPlay(string sceneName) 
    {
        sonidoBoton.PlayButtonSound();
        SceneManager.LoadScene(sceneName);
    }

    // Exits the game application
    public void ExitGame(){
        UnityEditor.EditorApplication.isPlaying = false;
        //Application.Quit();
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
