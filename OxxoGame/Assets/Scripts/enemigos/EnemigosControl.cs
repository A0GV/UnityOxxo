using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemigosControl : MonoBehaviour
{
    static public EnemigosControl Instance;
    public UIControlEnemigos uiController;

    void Start()
    {   
        StopAllCoroutines();
        Instance = this;
        uiController.ShowApuntar();
        init();
    }  
    void init(){
        if(uiController!=null){
            uiController.StartTime();
        }
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