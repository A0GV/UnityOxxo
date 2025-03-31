using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic; // To use library

// Clase usada para monitorear los problemas activos
public class ProblemaActivo
{
    public Problema datosProblema;
    public float tiempoInicio;
    public bool resuelto;

    // Constructor para el problema activo
    public ProblemaActivo(Problema datos, float _tiempoInicio)
    {
        datosProblema = datos;
        tiempoInicio = _tiempoInicio;
        resuelto = false;
    }
}

public class DiaControl : MonoBehaviour
{
    static public DiaControl Instance; // Instance de controller
    public UIControlDia uiController;

    // Dinero 
    public int dinero = 0; 

    // Lista de problemas activos 
    public List<ProblemaActivo> problemasActivos = new List<ProblemaActivo>();

    // Variables 
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
        PlayerPrefs.SetInt("dinero", PlayerPrefs.GetInt("dinero", dinero)); 
        Instance = this;
        DontDestroyOnLoad(this.gameObject); // Para no destruir instancia
        uiController.ShowCanva(); //muestra metricas del juego
        init();
    }

    //Inicia corrutinas
    void init(){
        if (uiController != null){
            uiController.StartTime();
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

    // Ecuación de dinero
    public void CalcularDinero() 
    {

    }

    // Manda a la escena final del juego
    public void Gotoendgame(){
        SceneManager.LoadScene("EndGamesScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
