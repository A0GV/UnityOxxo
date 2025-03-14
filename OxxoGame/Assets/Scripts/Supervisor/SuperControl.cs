using UnityEngine;
using UnityEngine.SceneManagement;

public class SuperControl : MonoBehaviour
{
    static public SuperControl Instance;
    public UIControlSuper uiController;

    void Start()
    {   
        StopAllCoroutines();
        Instance = this;
        uiController.ShowResumen();
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
