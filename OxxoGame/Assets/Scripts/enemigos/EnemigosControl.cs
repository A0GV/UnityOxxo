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
        SceneManager.LoadScene("MenuScene");
    }

    public void Gotoendgame()
    {
        SceneManager.LoadScene("EndGamesScene");
    }
}