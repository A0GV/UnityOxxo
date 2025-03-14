using UnityEngine;
using UnityEngine.SceneManagement;

public class DiaControl : MonoBehaviour
{
    static public DiaControl Instance;
    public UIControlDia uiController;
    //Regresa al menu
    public void EndMiniGame(){
        SceneManager.LoadScene("MenuScene");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //Termina corrutinas e inicializa variables
    void Start()
    {   
        StopAllCoroutines();
        PlayerPrefs.SetInt("preguntas",3);
        Instance = this;
        uiController.ShowCanva(); //muestra metricas del juego
        init();
    }
    //Inicia corrutinas
    void init(){
        if(uiController!=null){
            uiController.StartTime();
        }
    }
    //get para saber cuantas preguntas han sido contestadas
    public int Getcontestadas(){
        return PlayerPrefs.GetInt("preguntas",3);
    }
    //Contador para restar una pregunta de la cantidad total
    public void Contarpreguntas(){
        int newcontestadas = Getcontestadas() - 1;
        PlayerPrefs.SetInt("preguntas", newcontestadas);
        uiController.checarnumpreguntas();
    }
    //Manda a la escena final del juego
    public void Gotoendgame(){
        SceneManager.LoadScene("EndGamesScene");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
