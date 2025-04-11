using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class PreguntaManager : MonoBehaviour
{
    public static PreguntaManager Instance;
    public EnemigosSFX enemigosSFX; // Referencia al script de efectos de sonido
    public class Pregunta
    {
        public int id_preguntaenemigo; // ID único de la pregunta
        public string textoPregunta; // El texto de la pregunta
        public string[] opciones;   // Opciones de respuesta
        public int respuestaCorrecta; // Índice de la respuesta correcta (0-3)
    }

    public Text textoPreguntaUI; // Referencia al texto de la pregunta en la UI
    public Button[] botonesRespuestas; // Botones para las opciones de respuesta
    public List<Pregunta> preguntas; // Lista de preguntas
    public GameObject panelRespuestaCorrecta; // Panel para respuesta correcta
    public GameObject panelRespuestaIncorrecta; // Panel para respuesta incorrecta
    public GameObject panelTiempoAgotado; // Panel para tiempo agotado
    public GameObject panelRachaActiva; // Panel que indica que la racha está activa
    public GameObject panelRachaJuego; // Panel encima del juego para la racha
    public Text tiempoRestanteUI; // Texto para mostrar el tiempo restante
    private Pregunta preguntaActual; // La pregunta que se está mostrando actualmente
    private Pregunta ultimaPregunta; // Última pregunta mostrada

    public int maicesCorrectos = 0; // Contador de maíces (respuestas correctas)
    private UIControlEnemigos uiController; // Referencia al controlador de UI

    private float tiempoLimite = 10f; // Tiempo límite para responder cada pregunta
    private float tiempoRestante; // Tiempo restante para la pregunta actual
    private bool temporizadorActivo = false; // Controla si el temporizador está activo
    private bool rachaActiva = false; // Indica si la racha está activa

    private int rachaContador = 0; // Contador de respuestas correctas consecutivas

    void Start()
    {
        // Obtener referencia al controlador de UI
        uiController = FindFirstObjectByType<UIControlEnemigos>();

        // Obtener referencia a APIEnemigos
        APIEnemigos apiEnemigos = FindFirstObjectByType<APIEnemigos>();
        if (apiEnemigos != null)
        {
            StartCoroutine(apiEnemigos.CargarPreguntasDesdeAPI((preguntasCargadas) =>
            {
                if (preguntasCargadas != null)
                {
                    preguntas = preguntasCargadas;
                    MostrarNuevaPregunta();
                }
                else
                {
                    Debug.LogError("No se pudieron cargar las preguntas.");
                }
            }));
        }
        else
        {
            Debug.LogError("No se encontró el script APIEnemigos en la escena.");
        }
    }

    void Update()
    {
        if (temporizadorActivo)
        {
            tiempoRestante -= Time.deltaTime;
            tiempoRestanteUI.text = Mathf.Ceil(tiempoRestante).ToString("00") + " seg"; // Actualizar el texto del tiempo restante

            if (tiempoRestante <= 0)
            {
                TiempoAgotado();
            }
        }
    }

    void MostrarNuevaPregunta()
    {
        if (preguntas.Count == 0)
        {
            Debug.Log("No hay preguntas disponibles.");
            return;
        }

        // Seleccionar una pregunta al azar que no sea la misma que la última
        Pregunta nuevaPregunta;
        do
        {
            nuevaPregunta = preguntas[Random.Range(0, preguntas.Count)];
        } while (nuevaPregunta == ultimaPregunta && preguntas.Count > 1);

        preguntaActual = nuevaPregunta;
        ultimaPregunta = preguntaActual;

        // Barajar las respuestas
        List<int> indices = new List<int> { 0, 1, 2, 3 };
        Shuffle(indices);

        // Mostrar la pregunta y las opciones en la UI
        textoPreguntaUI.text = preguntaActual.textoPregunta;
        for (int i = 0; i < botonesRespuestas.Length; i++)
        {
            // Configurar el texto del botón
            botonesRespuestas[i].GetComponentInChildren<Text>().text = preguntaActual.opciones[indices[i]];

            // Asignar el nombre del botón para identificar si es correcto o incorrecto
            botonesRespuestas[i].name = indices[i] == preguntaActual.respuestaCorrecta ? "Correcta" : "Incorrecta";

            // Eliminar cualquier listener previo para evitar duplicados
            botonesRespuestas[i].onClick.RemoveAllListeners();

            // Capturar el índice actual y asignar el evento de clic
            int index = i;
            botonesRespuestas[i].onClick.AddListener(() => VerificarRespuesta(index));
        }

        // Reiniciar el temporizador
        tiempoRestante = tiempoLimite;
        temporizadorActivo = true;
    }

    void VerificarRespuesta(int indiceSeleccionado)
    {
        temporizadorActivo = false; // Detener el temporizador

        if (botonesRespuestas[indiceSeleccionado].name == "Correcta")
        {
            Debug.Log("Respuesta correcta!");
            maicesCorrectos++; // Incrementar el contador total de respuestas correctas
            rachaContador++;   // Incrementar el contador de racha

            // Verificar si la racha está activa
            if (rachaContador >= 5)
            {
                // Reproducir solo el sonido de racha
                if (enemigosSFX != null)
                {
                    enemigosSFX.PlayRachaEffect();
                }
            }
            else
            {
                // Reproducir el sonido de respuesta correcta si no hay racha activa
                if (enemigosSFX != null)
                {
                    enemigosSFX.PlayCorrectAnswer();
                }
            }

            // Actualizar la UI con el nuevo conteo de maíces
            if (uiController != null)
            {
                uiController.ActualizarMaices(maicesCorrectos);
            }

            // Aquí puedes otorgar una bala al jugador
            JugadorDisparo jugador = FindFirstObjectByType<JugadorDisparo>();
            if (jugador != null)
            {
                jugador.Recargar(1); // Otorgar una bala
            }

            // Verificar si se inicia una racha
            if (rachaContador >= 5)
            {
                if (!rachaActiva)
                {
                    IniciarRacha();
                }
                else
                {
                    // Si ya está en racha, ejecutar las acciones de la racha
                    AccionesRacha();

                    // Mostrar los paneles de racha nuevamente
                    StartCoroutine(MostrarPanelRacha());
                }
            }

            // Mostrar el panel de respuesta correcta
            StartCoroutine(MostrarPanelTemporal(panelRespuestaCorrecta));
        }
        else
        {
            Debug.Log("Respuesta incorrecta.");

            // Reproducir el sonido de respuesta incorrecta
            if (enemigosSFX != null)
            {
                enemigosSFX.PlayWrongAnswer();
            }

            // Terminar la racha si está activa
            if (rachaActiva)
            {
                TerminarRacha();
            }

            // Reiniciar el contador de racha
            rachaContador = 0;

            // Mostrar el panel de respuesta incorrecta
            StartCoroutine(MostrarPanelTemporal(panelRespuestaIncorrecta));
        }
    }

    void TiempoAgotado()
    {
        Debug.Log("Tiempo agotado.");
        temporizadorActivo = false; // Detener el temporizador

        // Reproducir el sonido de tiempo agotado
        if (enemigosSFX != null)
        {
            enemigosSFX.PlayWrongAnswer(); // Puedes usar el mismo sonido de respuesta incorrecta o uno diferente
        }

        // Terminar la racha si está activa
        if (rachaActiva)
        {
            TerminarRacha();
        }

        // Mostrar el panel de tiempo agotado
        StartCoroutine(MostrarPanelTemporal(panelTiempoAgotado));
    }

    void IniciarRacha()
    {
        Debug.Log("¡Racha iniciada!");
        rachaActiva = true;


        // Mostrar los paneles de racha
        StartCoroutine(MostrarPanelRacha());

        // Ejecutar las acciones iniciales de la racha
        AccionesRacha();
    }

    void AccionesRacha()
    {
        // Eliminar enemigos en pantalla y otorgar experiencia
        EnemyBehavior[] enemigos = FindObjectsByType<EnemyBehavior>(FindObjectsSortMode.None);
        int experienciaTotal = 0;

        foreach (var enemigo in enemigos)
        {
            experienciaTotal += enemigo.experiencePoints;
            Destroy(enemigo.gameObject);
        }

        // Otorgar experiencia al jugador
        PlayerExperience jugador = FindFirstObjectByType<PlayerExperience>();
        if (jugador != null)
        {
            jugador.AddExperience(experienciaTotal);
        }
    }

    IEnumerator MostrarPanelRacha()
    {
        // Asegurarse de que ambos paneles estén activos
        if (panelRachaActiva != null)
        {
            panelRachaActiva.SetActive(true);
            Debug.Log("Panel de racha activa mostrado.");
        }
        else
        {
            Debug.LogWarning("panelRachaActiva no está asignado.");
        }

        if (panelRachaJuego != null)
        {
            panelRachaJuego.SetActive(true);
            Debug.Log("Panel de racha encima del juego mostrado.");
        }
        else
        {
            Debug.LogWarning("panelRachaJuego no está asignado.");
        }

        // Esperar 1 segundo
        yield return new WaitForSeconds(1f);

        // Desactivar ambos paneles después del tiempo
        if (panelRachaActiva != null)
        {
            panelRachaActiva.SetActive(false);
            Debug.Log("Panel de racha activa ocultado.");
        }

        if (panelRachaJuego != null)
        {
            panelRachaJuego.SetActive(false);
            Debug.Log("Panel de racha encima del juego ocultado.");
        }
    }

    void TerminarRacha()
    {
        Debug.Log("Racha terminada.");
        rachaActiva = false;

        // Reiniciar el contador de racha
        rachaContador = 0;

        // Ocultar los paneles de racha
        if (panelRachaActiva != null)
        {
            panelRachaActiva.SetActive(false);
        }

        if (panelRachaJuego != null)
        {
            panelRachaJuego.SetActive(false);
        }
    }

    IEnumerator MostrarPanelTemporal(GameObject panel)
    {
        // Activar el panel
        panel.SetActive(true);

        // Esperar 1 segundo
        yield return new WaitForSeconds(1f);

        // Desactivar el panel
        panel.SetActive(false);

        // Mostrar una nueva pregunta después de que desaparezca el panel
        MostrarNuevaPregunta();
    }

    void Shuffle(List<int> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            int temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    [System.Serializable]
    private class PreguntaList
    {
        public List<Pregunta> preguntas;
    }
}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        string newJson = "{ \"array\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.array;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }
}