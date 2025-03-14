using UnityEngine;
using UnityEngine.SceneManagement;

public class DiaControl : MonoBehaviour
{
    static public DiaControl Instance;
    public UIControlDia uiController;
    public void EndMiniGame(){
        SceneManager.LoadScene("MenuScene");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        StopAllCoroutines();
        PlayerPrefs.SetInt("preguntas",3);
        Instance = this;
        uiController.ShowCanva();
        init();
    }
    void init(){
        if(uiController!=null){
            uiController.StartTime();
        }
    }
    public int Getcontestadas(){
        return PlayerPrefs.GetInt("preguntas");
    }
    public void Contarpreguntas(){
        int newcontestadas = Getcontestadas()-1;
        PlayerPrefs.SetInt("preguntas",newcontestadas);
        uiController.checarnumpreguntas();
    }
    public void Gotoendgame(){
        SceneManager.LoadScene("EndGamesScene");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
