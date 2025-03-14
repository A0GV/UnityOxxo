using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class UIControlDia : MonoBehaviour
{
    public GameObject canva;
    public GameObject pregunta;
    public GameObject resultados;
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
        resultados.SetActive(false);
    }

    public void ShowPregunta()
    {
        canva.SetActive(false);
        pregunta.SetActive(true);
        resultados.SetActive(false);
    }

    public void HidePregunta()
    {
        canva.SetActive(true);
        pregunta.SetActive(false);
        resultados.SetActive(false);
    }

    public void ShowResultados()
    {
        canva.SetActive(false);
        pregunta.SetActive(false);
        resultados.SetActive(true);
    }
}
