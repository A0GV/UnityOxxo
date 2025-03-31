using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class UIControlDia : MonoBehaviour
{
    public GameObject canva;
    public GameObject pregunta;
    public GameObject pausa;
    public int numpreguntas;
    public Text textDinero; // Texto auto-updating durante dia
    public Text textDineroQuestion; // Texto con cantidad de dinero durante pausa
    

    // De DiaControl
    int dinero;

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

    //Inicia corrutinas para mostrar la pregunta después de mostrar una pregunta
    public void StartTime() 
    {
        StartCoroutine(StartDay());
    }

    // Enseñar dinero durante día
    public void ShowMoney()
    {
        textDinero.text = "$ " + dinero; 
    }

    // Enseñar dinero en pantalla de desición
    public void ShowQuestionMoney()
    {
        textDineroQuestion.text = "$ " + dinero; 
    }

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
            StartCoroutine(StartDay()); // Starts the day again
        } 
        // Else has not finished day
        else 
        {
            StartCoroutine(StartDay()); // Calls routine again
        }
    }
 
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
    //muestra preguntas y apaga muestras
    public void ShowPregunta()
    {
        canva.SetActive(false);
        pregunta.SetActive(true);
    }

    // Esconde la pregunta y cuenta las preguntas que han salido
    public void HidePregunta()
    {
        canva.SetActive(true);
        pregunta.SetActive(false);
        DiaControl.Instance.Contarpreguntas();
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
