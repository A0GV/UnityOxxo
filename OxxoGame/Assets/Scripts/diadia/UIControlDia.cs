using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class UIControlDia : MonoBehaviour
{
    public GameObject canva;
    public GameObject pregunta;
    public GameObject pausa;
    public int numpreguntas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Inicia corrutinas para mostrar la pregunta después de mostrar una pregunta
    public void StartTime(){
        StartCoroutine(Mostrarpregunta());
    }

    //Espera 5 segundos para mostrar la pregunta
    IEnumerator Mostrarpregunta(){
        yield return new WaitForSeconds(12);
        ShowPregunta();
        StartCoroutine(Mostrarpregunta());
    }

    //Muestra metricas del juego y apaga el canva de preguntas
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
    //Esconde la pregunta y cuenta la
    // s preguntas que han salido
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
