using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class UIControlDia : MonoBehaviour
{
    // Instance
    static public DiaControl Instance; // Instance de controller

    public GameObject canva;
    public GameObject pregunta;
    public GameObject pausa;
    public int numpreguntas;
    public Text textDinero; // Texto auto-updating durante dia
    public Text textDineroQuestion; // Texto con cantidad de dinero durante pausa

    public Text textAct1; 
    public Text textAct2; 
    public Text textAct3;

    public int dineroUI; 
    
    // De DiaControl
    int dinero;

    // Usado en DiaControl
    public bool answered = false; // Sets question answered to false 

    // Local to file
    int time = 0; // To wait exactly 12 seconds every day 


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dinero = DiaControl.Instance.dinero;
        ShowMoney(); // Changes money text
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Enseñar dinero durante día
    public void ShowMoney()
    {
        dineroUI = DiaControl.Instance.dinero + DiaControl.Instance.dineroDiaActual; // Uses instance of DiaControl dinero stored and adds the one that is being calculated every second to update by parts 
        textDinero.text = "$ " + dineroUI; 
    }

    // Enseñar dinero en pantalla de desición
    public void ShowQuestionMoney()
    {
        dineroUI = DiaControl.Instance.dinero; // Uses dinero cumulativo value
        textDineroQuestion.text = "$ " + dineroUI; 
    }

    // Muestra metricas del juego y apaga el canva de preguntas
    public void ShowCanva()
    {
        canva.SetActive(true);
        pregunta.SetActive(false);
        pausa.SetActive(false);
    }

    // Function to set skip day
    public void SkipDay() 
    {
        DiaControl.Instance.daySkipped = true;
        DiaControl.Instance.SkipCalcularDinero(); // Llama función indicando q se skip el día y actualize cantidad de dinero
        ShowMoney(); // Shows money just in case
        ShowPregunta(); 
    }

    // Muestra las preguntas
    public void ShowPregunta()
    {
        //answered = false; // Sets answered to false until answers something 
        canva.SetActive(false);
        pregunta.SetActive(true);

        // Shows money
        ShowQuestionMoney(); 

        // Shows problem names
        string act1Txt = DiaControl.Instance.problemasActivos[0].GetNombreProblema(); // First active problem
        textAct1.text = act1Txt;
        string act2Txt = DiaControl.Instance.problemasActivos[1].GetNombreProblema(); // Second active problem
        textAct2.text = act2Txt;
        string act3Txt = DiaControl.Instance.problemasActivos[2].GetNombreProblema(); // Tercer problema activo
        textAct3.text = act3Txt;
    }

    // Esconde la pregunta y cuenta las preguntas que han salido
    public void HidePregunta()
    {
        canva.SetActive(true); // Switches a vista tienda 
        pregunta.SetActive(false); // turns off the question
        DiaControl.Instance.Contarpreguntas(); // Updates number of questions 
        DiaControl.Instance.GenerarProblemasDelDia(); // Adds a new problem to list from controller

        // If day was not skipped, resets money and time 
        if (!DiaControl.Instance.daySkipped)
        {
            DiaControl.Instance.dineroDiaActual = 0;
            DiaControl.Instance.time = 0;
        }
        
        DiaControl.Instance.daySkipped = false; // Sets back to day has not been skipped flag 
        //DiaControl.Instance.dayCoroutine = DiaControl.Instance.StartCoroutine(DiaControl.Instance.StartDay()); // Restarts day
        DiaControl.Instance.StartDay(); // Starts day now without coroutine
    }

    //checa si ya se contestaron la cantidad de preguntas determinadas y 
    // en caso de que sí, manda a la escena final del juego
    public void checarnumpreguntas(){
        if(PlayerPrefs.GetInt("preguntas") <= 0){
            ShowResultados();
        }
    }

    //Manda a la escena final del juego
    public void ShowResultados()
    {
        DiaControl.Instance.Gotoendgame();
    }

    //Manda al menú
    public void Gotomenu()
    {
        DiaControl.Instance.EndMiniGame();
    }

    //Pausa
    public void pausado()
    {
        canva.SetActive(false);
        pregunta.SetActive(false);
        pausa.SetActive(true);
        DiaControl.Instance.StopCoroutine(DiaControl.Instance.dayCoroutine); // Uses instance to stop the dayCoroutine reference
    }

    // Resume game
    public void despausado()
    {
        canva.SetActive(true);
        pausa.SetActive(false);
        pregunta.SetActive(false);
        DiaControl.Instance.dayCoroutine = DiaControl.Instance.StartCoroutine(DiaControl.Instance.ResumeDay()); // To resume, must create a new instance and save reference in dayCoroutine agian

    }
}

/*
TODO
- Make it so that DDResolver can track which problem is clicked and eliminates from list, and generate a new problem
- Figure out how to place the danger icons in the right place 
- Change it so that it waits a bit to generate a new problem (once icons work)
- Add a comparison model for the points so that it is easier to calculate the amount of corn they win based on how well they answered, or a number to keep track of their priorization
*/