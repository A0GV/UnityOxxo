using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemigosControl : MonoBehaviour
{
    static public EnemigosControl Instance;
    public UIControlEnemigos uiController;

    void Start()
    {   

    }  
    void init(){

    }


    public void EndMiniGame()
    {
        Debug.Log("Regresando al menú...");
        Time.timeScale = 1; // Asegurarse de que el tiempo esté en escala normal
        SceneManager.LoadScene("MenuScene");
    }

    public void Gotoendgame()
    {
        Debug.Log("Cargando la escena de fin del juego...");
        Time.timeScale = 1; // Asegurarse de que el tiempo esté en escala normal
        SceneManager.LoadScene("EndGamesScene");
    }
}