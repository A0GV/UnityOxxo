using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScene : MonoBehaviour
{  
    // Sonido
    private AudioSource audioSource;
    public AudioClip buttonSound;
    Text eloteText;

    // Poner sonido
    void Awake()
    {
        // Configuración inicial del audio
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
    }

    //Manda a la escena de juego seleccionada
    public void StartToPlay(string sceneName) 
    {
        //PlayButtonSound();
        SceneManager.LoadScene(sceneName);
    }

    // Exits the game application
    public void ExitGame(){
        UnityEditor.EditorApplication.isPlaying = false;
        //Application.Quit();
    }

    // Método para reproducir el sonido de botón
    public void PlayButtonSound()
    {
        audioSource.PlayOneShot(buttonSound, 0.7f);
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
