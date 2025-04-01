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

    /* Inicia corrutinas para mostrar la pregunta después de mostrar una pregunta
    public void StartTime() 
    {
        StartCoroutine(StartDay());
    }
    */

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

    /*
    // New time function
    IEnumerator StartDay()
    {
        yield return new WaitForSeconds(1); // Waits one second
        time += 1; // Increases time by 1
        ShowMoney(); // Update money text

        // Checks if 12 seconds have passed
        if (time == 12)
        {
            ShowPregunta(); // Shows new question
            time = 0; // Resets time to start a new day
            DiaControl.Instance.GenerarProblemasDelDia(); // Calls instance to generate new problem
            StartCoroutine(StartDay()); // Starts the day again
        } 
        // Else has not finished day
        else 
        {
            StartCoroutine(StartDay()); // Calls routine again
        }
    }
    */
 
    //Espera 5 segundos para mostrar la pregunta
    /*
    IEnumerator Mostrarpregunta()
    {
        yield return new WaitForSeconds(12);
        ShowPregunta();
        StartCoroutine(Mostrarpregunta());
    }
    */

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
        DiaControl.Instance.time = 0; // Reset time counting
        DiaControl.Instance.dayCoroutine = DiaControl.Instance.StartCoroutine(DiaControl.Instance.StartDay()); // Restart timer
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
    public void pausado(){
        canva.SetActive(false);
        pregunta.SetActive(false);
        pausa.SetActive(true);
    }

    //MOniii, este lo estoy usando de que en el boton para continuar y el de reiniciar, 
    // pero se ocupa otraaa función para que eso jale realmente bien, tqm!! 
    public void despausado(){
        canva.SetActive(true);
        pausa.SetActive(false);
        pregunta.SetActive(false);
    }
}

/*
TODO
- Figure out how to place the danger icons in the right place 
- Make sure the equation works
*/