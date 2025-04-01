using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Security.Cryptography; // To use library
public class DiaControl : MonoBehaviour
{
    static public DiaControl Instance; // Instance de controller
    public UIControlDia uiController;

    // Dinero 
    public int dinero = 0; 
    int time = 0; 
    public int dineroSkip = 0; // Para enseñar la animación

    // Monitorear problemas
    public List<Problema> todosProblemas = new List<Problema>(); // Lista de todos los problemas posibles
    public List<Problema> problemasActivos = new List<Problema>(); // Lista de problemas activos 
    int numProblemasActivos = 0; // Monitorea número de problemas activos

    // Valor máximo de cada tipo de problema
    public int totalSatisfaction; 
    public int planograma = 7; 
    public int expiradoRetiro = 8;
    public int maquinasFuncionales = 9;
    public int cajerosHorario = 8;
    public int cajerosFinanzas = 5;
    public int horarioPuntual = 10;
    public int ejecucionPromo = 6; 
    public int limpieza = 10; 
    public int atencionCliente = 8;

    //Regresa al menu
    public void EndMiniGame(){
        SceneManager.LoadScene("MenuScene");
    }

    //Termina corrutinas e inicializa variables, cambiado Start -> Awake 
    void Awake()
    {   
        StopAllCoroutines();
        PlayerPrefs.SetInt("preguntas", 8);
        PlayerPrefs.SetInt("dinero", 0); // Sets to 0
        Instance = this;
        DontDestroyOnLoad(this.gameObject); // Para no destruir instancia
        uiController.ShowCanva(); //muestra metricas del juego
        //DeclararProblemas(); // Function to help initialize all possible problems
        problemasActivos.Clear(); // Make sure no problems are left over
        init();
    }

    /*
    public void DeclararProblemas()
    {
        Problema p1 = new Problema("Planograma", -3, "No se está siguiendo una parte del planograma"); 
        todosProblemas.Append<p1>; 
    }
    */

    //Inicia corrutinas
    void init(){
        /*
        if (uiController != null){
            uiController.StartTime(); // Starts timer
        }
        */
        GenerarProblemasDelDia(); 
        StartCoroutine(StartDay()); 
    }

    // Starts a new day
    IEnumerator StartDay()
    {
        yield return new WaitForSeconds(1); // Waits one second
        time += 1; // Increases time by 1
        uiController.ShowMoney(); // Update money text on UI

        // Checks if 12 seconds have passed
        if (time == 12)
        {
            uiController.ShowPregunta(); // Shows new question
            time = 0; // Resets time to start a new day
            //DiaControl.Instance.GenerarProblemasDelDia(); // Calls instance to generate new problem
            GenerarProblemasDelDia(); // Generates new problems
            StartCoroutine(StartDay()); // Starts the day again
        } 
        // Else has not finished day
        else 
        {
            StartCoroutine(StartDay()); // Calls routine again
        }
    }
    
    // Get para saber cuantas preguntas han sido contestadas
    public int Getcontestadas(){
        return PlayerPrefs.GetInt("preguntas", 8);
    }

    //Contador para restar una pregunta de la cantidad total
    public void Contarpreguntas(){
        int newcontestadas = Getcontestadas() - 1;
        PlayerPrefs.SetInt("preguntas", newcontestadas);
        uiController.checarnumpreguntas();
    }

    // Ecuación de dinero por segundo
    public int CalcularDinero() 
    {
        // Sums all variables to calculate earnings
        dinero += planograma + expiradoRetiro + maquinasFuncionales + cajerosHorario + cajerosFinanzas + horarioPuntual + ejecucionPromo + limpieza + atencionCliente;

        // Bajar cantidad de dinero en base al negative impact de cada uno
        /*
        numProblemasActivos = problemasActivos.Count; // Get number of active problems
        for (int i = 0; i < numProblemasActivos; i++)
        {
            dinero += problemasActivos[i].GetImpactoNegativo(); // Reduces the amount of money earned based on active problems
        }
        */

        return dinero; // Returns money 
    }

    // Calcula cantidad todal de dinero por si usuario se salta todo, actualiza la info
    public void SkipCalcularDinero() 
    {

        // Sums all variables to calculate earnings
        dineroSkip += (planograma + expiradoRetiro + maquinasFuncionales + cajerosHorario + cajerosFinanzas + horarioPuntual + ejecucionPromo + limpieza + atencionCliente) * (12-time); // Usa los 12 segundos - tiempo q ya paso por si se salta el dinero

        /*
        // Bajar cantidad de dinero en base al negative impact de cada uno
        numProblemasActivos = problemasActivos.Count; // Get number of active problems
        for (int i = 0; i < numProblemasActivos; i++)
        {
            dinero += problemasActivos[i].GetImpactoNegativo(); // Reduces the amount of money earned based on active problems
        }
        */

        dinero += dineroSkip; // Agrega dinero del skip al principal

        //return dineroSkip; // Returns money 
    }

    // Manejar problemas
    public void GenerarProblemasDelDia()
    {
        int diaActual = 9 - PlayerPrefs.GetInt("preguntas", 8); // Día 1 a 8

        // If in day 1, generates three questions
        if (PlayerPrefs.GetInt("preguntas") == 8)
        {
            problemasActivos.Clear();

            // To eliminate option of repeating problems 
            List<Problema> copiaProblemas = new List<Problema>(todosProblemas);

            for (int i = 0; i < 3; i++)
            {
                int randomIndex = UnityEngine.Random.Range(0, copiaProblemas.Count); // Random index of problem
                Problema seleccionado = copiaProblemas[randomIndex]; // Stores Problema selected randomly
                copiaProblemas.RemoveAt(randomIndex); // Eliminates for tracking
                problemasActivos.Add(seleccionado); // Adds random problem
            }
        }
        // Remaining days 
        else
        {
            if (problemasActivos.Count < 3)
            {
                List<Problema> disponibles = todosProblemas.Except(problemasActivos).ToList(); // Stores all problems that were created but excludes active ones to avoid repetition
                int randomIndex = UnityEngine.Random.Range(0, disponibles.Count);
                Problema seleccionadoNuevo = disponibles[randomIndex];
                problemasActivos.Add(seleccionadoNuevo);
            }
        }
    }

    // Resolver problema

    // Manda a la escena final del juego
    public void Gotoendgame(){
        SceneManager.LoadScene("EndGamesScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}