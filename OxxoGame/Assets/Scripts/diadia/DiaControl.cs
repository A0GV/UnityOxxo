/* 
Future things 
* Make it so that once user selects answer, the buttons change colors so that the ones that were more dangerous, detract more points, have a different color (yellow, orange, red)
* Make it so that the user can't skip the days until an active problem has arisen
*/ 
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Security.Cryptography;
using System;
using Unity.Burst.CompilerServices; // To use library

public class DiaControl : MonoBehaviour
{
    // Instances 
    static public DiaControl Instance; // Instance de controller
    public UIControlDia uiController;
    public DDResolver resolverInstance; 
    public DDSpawner spawnerInstance;
    public DDAPI apiInstance; 

    // To reload game
    public GameObject storeView;

    // Control dinero 
    public int dinero; // Total money earned
    public int dineroDiaActual; // Money earned during that day 

    // Control de tiempo
    public int timePerDay = 12; // Sets day length
    public int time; // Public because needed in UI control to reset time
    public bool checkDayActive = true; // Used to check if day is active 
    public Coroutine dayCoroutine; // To track the day coroutine
    public bool daySkipped = false; // Checks if user skipped day animation

    public bool gameOver = false; // Sets game over to false 
    
    // User money
    public int elotesGanados; 
    public int expGanado; 

    // Problemas
    public GameObject problemaPrefab; // Prefab de danger icon
    public List<Problema> todosProblemas = new List<Problema>(); // Lista de todos los problemas posibles
    public List<Problema> problemasActivos = new List<Problema>(); // Lista de problemas activos 
    int numProblemasActivos = 0; // Monitorea número de problemas activos

    // Using API connection
    public int id_usuario = 7; // Will be using 7 until login is complete
    public int diasJugados = 0; // Días q ha jugado este juego
    public int expExtra = 0; // Extra exp based on number of times played before

    

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

    // Si usuario reinicia el día, solo se vuelve a llamar awake para destruir la info, no funciona bien tho
    public void ReiniciarDia()
    {
        dinero = 0;
        elotesGanados = 0; 
        expGanado = 0;
        //Awake();
        SceneManager.LoadScene( SceneManager.GetActiveScene().name );
    }

    //Termina corrutinas e inicializa variables, cambiado Start -> Awake 
    void Awake()
    {   
        StopAllCoroutines();
        // Reset values 
        time = 0; 
        dineroDiaActual = 0;
        PlayerPrefs.SetInt("preguntas", 8);
        PlayerPrefs.SetInt("dinero", 0); // Sets to 0
        PlayerPrefs.SetInt("countAlta", 0); 
        PlayerPrefs.SetInt("countMed", 0); 
        PlayerPrefs.SetInt("countLow", 0);

        problemasActivos.Clear(); // Make sure no problems are left over

        // Instances
        Instance = this;
        DontDestroyOnLoad(this.gameObject); // Para no destruir instancia
        //StartCoroutine(apiInstance.GetDaysPlayed());

    }

    public int CalcularExpExtra() 
    {
        return 0; 
    }

    void Start()
    {
        // Sets all items to render off
        for (int i = 0; i < todosProblemas.Count; i++)
        {
            todosProblemas[i].SetRenderStatus(false); 
        }

        uiController.ShowCanva(); // Métricas del juego
        init(); // Starts day 
    } 

    // Inicia todo 
    void init()
    {
        StartDay(); // Inicia día 
    }

    // Starts a new day, public to restart day from UIControl IEnumerator
    public void StartDay()
    {
        //StopDayCoroutine(); // Stops just in case 

        checkDayActive = true; // Starts the day 
        time = 0; // Ensures time is reset just in case
        dineroDiaActual = 0; // Resets day earnings 

        // Updates prioridades ya resueltas de UI
        uiController.UpdatePrioridad(); 

        // Problem handling
        GenerarProblemasDelDia(); // Sets new problems
        SpawnIconProblem(); // Renders icons

        // Crea ajolote
        spawnerInstance.StartSpawning();

        /*
        for (int i = 0; i < problemasActivos.Count; i++) 
        {
            Debug.Log(problemasActivos[i].GetNombreProblema());
        }
        */

        // If day is active and has not ended yet, starts the day
        if (checkDayActive && !gameOver)
        {
            dayCoroutine = StartCoroutine(ResumeDay()); // Stores coroutine reference to be able to stop it, useful to manipulate it in other parts of the program without causing coroutine errors (having multiple active and such) 
        }
        else 
        {
            Debug.Log("Day is not active");
        }
        
    }

    // Coroutine para manejar el tiempo del día
    public IEnumerator ResumeDay()
    {
        // Just doesn't reset values
        checkDayActive = true; 

        while (checkDayActive && !daySkipped)
        {
            yield return new WaitForSeconds(1); // Waits one second
            time += 1; // Increases time by 1
            dineroDiaActual += CalcularSatisfaccionPorSegundo(); // Updates money earned every second based on active problems
            uiController.ShowMoney(); // Update money text on UI

            // Checks if day is over seconds have passed
            if (time >= timePerDay)
            {
                checkDayActive = false;
                dinero += dineroDiaActual;
                uiController.ShowPregunta();
                yield break; // Exits coroutine just in case
            }
        }
        yield break; // Exits coroutine just in case
    }
    
    // Get para saber cuantas preguntas han sido contestadas
    public int Getcontestadas()
    {
        return PlayerPrefs.GetInt("preguntas", 8);
    }

    //Contador para restar una pregunta de la cantidad total
    public void Contarpreguntas(){
        int newcontestadas = Getcontestadas() - 1;
        PlayerPrefs.SetInt("preguntas", newcontestadas);
        uiController.checarnumpreguntas();
    }

    // Calcula las ganancias por segundo
    public int CalcularSatisfaccionPorSegundo()
    {
        // Sums all variables to calculate earnings for each second, m in y = mx
        int satisfaccionPorSegundo = planograma + expiradoRetiro + maquinasFuncionales + cajerosHorario + cajerosFinanzas + horarioPuntual + ejecucionPromo + limpieza + atencionCliente;

        // Bajar cantidad de dinero en base al negative impact de cada uno
        numProblemasActivos = problemasActivos.Count; // Get number of active problems
        for (int i = 0; i < numProblemasActivos; i++)
        {
            satisfaccionPorSegundo += problemasActivos[i].GetImpactoNegativo(); // Reduces the amount of money earned based on active problems
        }

        return satisfaccionPorSegundo; // To update count dynamically based on seconds, or once has 12 seconds
    }

    // Calcula cantidad todal de dinero por si usuario se salta todo, actualiza la info
    public void SkipCalcularDinero() 
    {
        daySkipped = true; // Ensure variable for skip is set

        int tiempoRestante = timePerDay - time; // Checks how much time was left before the skip
        int satisfaccionPorSegundo = CalcularSatisfaccionPorSegundo(); // Guarda satisfaccion por segundo del día
        dineroDiaActual += satisfaccionPorSegundo * tiempoRestante;
        dinero += dineroDiaActual; // Update dinero total

        // Reset values 
        dineroDiaActual = 0; // Resets money for day
        StopCoroutine(dayCoroutine); // Stops the coroutine
        checkDayActive = false; // Stops the day properly 

        // UI controllers 
        uiController.ShowMoney(); // Shows money text
        uiController.ShowPregunta(); // Shows questioin
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
                problemasActivos[i].SetRenderStatus(true); // Sets problem as active to render it 
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
                problemasActivos[2].SetRenderStatus(true); // Makes it so that it does render
            }
        }

        // Calls to check priorities
        AsignarPrioridades();
    }

    public void AsignarPrioridades() 
    {
        Debug.Log("Asignando prioridades a " + problemasActivos.Count + " problemas:");

        if (problemasActivos.Count == 3)
        {
            // Apparently hay manera de ordenar por valor usando Linq, es lista que referencia a los valores originales 
            List<Problema> problemasOrdenados = problemasActivos.OrderBy(currentProblem => currentProblem.GetImpactoNegativo()).ToList(); 

            // Guardar problemas
            Problema p1 = problemasOrdenados[0];
            Problema p2 = problemasOrdenados[1];
            Problema p3 = problemasOrdenados[2];
            
            // Guarda impacto negativo de los problemas
            int negativo1 = problemasOrdenados[0].GetImpactoNegativo(); 
            int negativo2 = problemasOrdenados[1].GetImpactoNegativo(); 
            int negativo3 = problemasOrdenados[2].GetImpactoNegativo(); 

            // Casos de prioridad
            // Orden 1 mayor negativo, 2 medio, 3 menor impacto negativo
            if (negativo1 < negativo2 && negativo2 < negativo3)
            {
                p1.SetPrioridad(3);
                p2.SetPrioridad(2);
                p3.SetPrioridad(1);
            }
            // Todos mismo valor
            else if (negativo1 == negativo2 && negativo2 == negativo3)
            {
                p1.SetPrioridad(3);
                p2.SetPrioridad(3);
                p3.SetPrioridad(3);
            }
            // Negativo 1 y 2 más negativo, 3 menor
            else if (negativo1 == negativo2 && negativo2 < negativo3)
            {
                p1.SetPrioridad(3);
                p2.SetPrioridad(3);
                p3.SetPrioridad(2);
            }
            // Negativo 1 más alto, 2 y 3 menor
            else if (negativo1 < negativo2 && negativo2 == negativo3)
            {
                p1.SetPrioridad(3);
                p2.SetPrioridad(2);
                p3.SetPrioridad(2);
            }

            for (int i = 0; i < 3; i++)
            {
                Debug.Log("Problema " + i + " " + problemasOrdenados[i].GetNombreProblema()?.ToString() + ": " + problemasOrdenados[i].GetImpactoNegativo().ToString() + " con " + problemasOrdenados[i].GetPrioridad().ToString());
            }
        }
        else 
        {
            Debug.Log("Problema asignando valores, cantidad de problemas:" + problemasActivos.Count);
        }
    }

    // Ícono de problemas
    public void SpawnIconProblem()
    {
        ClearDangerIcons(); // Erases previous ones before starting

        numProblemasActivos = problemasActivos.Count; // Get number of active problems
        //Debug.Log(numProblemasActivos);
        for (int i = 0; i < numProblemasActivos; i++)
        {
            if (problemasActivos[i].GetRenderStatus())
            {
            // Instantiates the problem
            GameObject nuevoProblema = Instantiate(problemaPrefab, problemasActivos[i].posicion, Quaternion.identity);
            // Renders sprite
            SpriteRenderer sr = nuevoProblema.GetComponent<SpriteRenderer>();
            sr.sprite = problemasActivos[i].icon; 
            }
        }
    }

    // To erase danger icons
    void ClearDangerIcons()
    {
        // Looks for game obj with danger tag
        GameObject[] problemasIcons = GameObject.FindGameObjectsWithTag("VisualDanger");
        foreach (GameObject icon in problemasIcons)
        {
            Destroy(icon);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}