using UnityEngine;
using UnityEngine.SceneManagement;

public class DiaControl : MonoBehaviour
{
    static public DiaControl Instance;
    public UIControlDia uiController;
    void EndMiniGame(){
        SceneManager.LoadScene("MenuScene");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        uiController.ShowCanva();
        init();
    }
    void init(){
        if(uiController!=null){
            uiController.StartTime();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
