using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class UIControlDia : MonoBehaviour
{
    // Instance
    static public DiaControl Instance; // Instance de controller

    // Pop-ups
    public GameObject canva;
    public GameObject pregunta;
    public GameObject pausa;
    public GameObject menuResultados;

    // Texto dinero
    public Text textDinero; // Texto auto-updating durante dia
    public Text textDineroQuestion; // Texto con cantidad de dinero durante pausa

    // Texto prioridedes in-game
    public Text countTextHigh; 
    public Text countTextMed; 
    public Text countTextLow;

    // Texto prioridades menú final
    public Text finalTextHigh; 
    public Text finalTextMed; 
    public Text finalTextLow;


    // Texto de exp y elote pausa
    public Text textElote; 
    public Text textExp;

    // Texto panel final
    public Text textFinalElote; 
    public Text textFinalExp;

    // Texto de título
    public Text textAct1; 
    public Text textAct2; 
    public Text textAct3;

    // Texto de descripción
    public Text textDesc1; 
    public Text textDesc2; 
    public Text textDesc3; 

    // Calculo de dinero
    public int dineroUI; 
    
    // De DiaControl
    int dinero;

    // Usado en DiaControl
    public bool answered = false; // Sets question answered to false 

    // Para manage problemas desde UI
    Problema[] problemasMostrados = new Problema[3];

    public Button botonSolve1;
    public Button botonSolve2;
    public Button botonSolve3;

    public DDButtonSolve1 botonScript1; 
    public DDButtonSolve2 botonScript2; 
    public DDButtonSolve3 botonScript3; 




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

    // Enseñar cuenta de prioridades resueltas
    public void UpdatePrioridad()
    {
        countTextHigh.text = PlayerPrefs.GetInt("countAlta", 0).ToString(); 
        countTextMed.text = PlayerPrefs.GetInt("countMed", 0).ToString(); 
        countTextLow.text = PlayerPrefs.GetInt("countLow", 0).ToString(); 
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
        menuResultados.SetActive(false);
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

        // Links name and description where there are only 3 active problems and checkign w count just in case
        for (int i = 0; i < DiaControl.Instance.problemasActivos.Count && i < 3; i++)
        {
            Problema problemaUI = DiaControl.Instance.problemasActivos[i]; // Gets problemas activos 
            problemasMostrados[i] = problemaUI; // Adds a arreglo de UI

            string problemName = problemaUI.GetNombreProblema(); // Stores problem name
            string problemDesc = problemaUI.GetDescripcionProblema(); // Gets problem description

            // Uses index to set name, desc, and then link problem to a button
            if (i == 0) 
            {
                textAct1.text = problemName;
                textDesc1.text = problemDesc; 
                botonScript1.SetProblema(problemaUI);
            }
            if (i == 1) 
            {
                textAct2.text = problemName;
                textDesc2.text = problemDesc; 
                botonScript2.SetProblema(problemaUI);
            }
            if (i == 2) 
            {
                textAct3.text = problemName;
                textDesc3.text = problemDesc; 
                botonScript3.SetProblema(problemaUI);
            }
        }
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
        
        // If day is not currently active and the game is not over
        if (!DiaControl.Instance.checkDayActive && !DiaControl.Instance.gameOver)
        {
            DiaControl.Instance.StartDay(); // Starts day if it is active 
        }
    }

    //checa si ya se contestaron la cantidad de preguntas determinadas y 
    // en caso de que sí, manda a la escena final del juego
    public void checarnumpreguntas(){
        if(PlayerPrefs.GetInt("preguntas") <= 0){
            ShowResultados();
        }
    }

    // Manda a la escena final del juego
    public void ShowResultados()
    {
        // Frena el tiempo y cambia day
        DiaControl.Instance.StopCoroutine(DiaControl.Instance.dayCoroutine); // Uses instance to stop the dayCoroutine reference
        DiaControl.Instance.gameOver = true; // Sets game as ended

        canva.SetActive(false);
        pausa.SetActive(false);
        pregunta.SetActive(false);
        menuResultados.SetActive(true);

        // Valores para BD 
        textFinalElote.text = DiaControl.Instance.elotesGanados.ToString(); 
        textFinalExp.text = DiaControl.Instance.expGanado.ToString(); 
        
        // Valores de prioridades 
        finalTextHigh.text = PlayerPrefs.GetInt("countAlta", 0).ToString(); 
        finalTextMed.text = PlayerPrefs.GetInt("countMed", 0).ToString(); 
        finalTextLow.text = PlayerPrefs.GetInt("countLow", 0).ToString(); 
    }

    // Reiniciar no guarda cantidad de elotes ni EXP, vuelve a empezar de 0
    public void RestartGame()
    {
        canva.SetActive(true);
        pausa.SetActive(false);
        pregunta.SetActive(false);
        menuResultados.SetActive(false);
        DiaControl.Instance.ReiniciarDia(); 
    }


    // Manda al menú de resultados si se pica en fin
    public void Gotomenu()
    {
        // Save elotes and exp amount amount
        SceneManager.LoadScene("MenuScene");
    }

    // Pausa
    public void pausado()
    {
        canva.SetActive(false);
        pregunta.SetActive(false);
        pausa.SetActive(true);
        menuResultados.SetActive(false);
        DiaControl.Instance.StopCoroutine(DiaControl.Instance.dayCoroutine); // Uses instance to stop the dayCoroutine reference
        // Valores acumulados 
        /*
        textElote.text = PlayerPrefs.GetInt("elotes").ToString(); 
        textExp.text = PlayerPrefs.GetInt("exp").ToString(); 
        */
        textElote.text = DiaControl.Instance.elotesGanados.ToString(); 
        textExp.text = DiaControl.Instance.expGanado.ToString(); 
    }

    // Resume game
    public void despausado()
    {
        canva.SetActive(true);
        pausa.SetActive(false);
        pregunta.SetActive(false);
        menuResultados.SetActive(false);
        DiaControl.Instance.dayCoroutine = DiaControl.Instance.StartCoroutine(DiaControl.Instance.ResumeDay()); // To resume, must create a new instance and save reference in dayCoroutine agian


    }
}

/*
TODO
- Also missing the restart day
- At game start, pull num of exp from DB to set a int multiplicador that will round exp to int but still be like number of times user has played so adds the num of days to the corn calculated
*/