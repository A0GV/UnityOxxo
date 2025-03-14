using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class UIControlDia : MonoBehaviour
{
    public GameObject canva;
    public GameObject pregunta;
    public int numpreguntas;
    private int contestadas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTime(){
        StartCoroutine(Mostrarpregunta());
    }

    IEnumerator Mostrarpregunta(){
        yield return new WaitForSeconds(5);
        ShowPregunta();
        StartCoroutine(Mostrarpregunta());
    }
    public void ShowCanva()
    {
        canva.SetActive(true);
        pregunta.SetActive(false);
    }

    public void ShowPregunta()
    {
        canva.SetActive(false);
        pregunta.SetActive(true);
    }

    public void HidePregunta()
    {
        canva.SetActive(true);
        pregunta.SetActive(false);
    }
    public void checarnumpreguntas(){
        if(PlayerPrefs.GetInt("preguntas")==0){
            ShowResultados();
        }
    }
    public void ShowResultados()
    {
        DiaControl.Instance.Gotoendgame();
    }
    public void Gotomenu()
    {
        DiaControl.Instance.EndMiniGame();
    }
}
