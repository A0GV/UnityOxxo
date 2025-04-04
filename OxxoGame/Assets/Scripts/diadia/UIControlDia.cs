using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


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

    // Texto de título
    public Text textAct1; 
    public Text textAct2; 
    public Text textAct3;

    // Texto de descripción
    public Text textDesc1; 
    public Text textDesc2; 
    public Text textDesc3; 

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

        /*
        // Shows problem names
        string act1Txt = DiaControl.Instance.problemasActivos[0].GetNombreProblema(); // First active problem
        textAct1.text = act1Txt;
        string act2Txt = DiaControl.Instance.problemasActivos[1].GetNombreProblema(); // Second active problem
        textAct2.text = act2Txt;
        string act3Txt = DiaControl.Instance.problemasActivos[2].GetNombreProblema(); // Tercer problema activo
        textAct3.text = act3Txt;
        */
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

    // Reiniciar no guarda cantidad de elotes ni EXP, vuelve a empezar de 0
    public void RestartGame()
    {
        canva.SetActive(true);
        pausa.SetActive(false);
        pregunta.SetActive(false);
        DiaControl.Instance.ReiniciarDia(); 
    }


    // Manda al menú de resultados si se pica en fin
    public void Gotomenu()
    {
        // Save elotes amount
        SceneManager.LoadScene("MenuScene");
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
- Also missing the restart day
*/