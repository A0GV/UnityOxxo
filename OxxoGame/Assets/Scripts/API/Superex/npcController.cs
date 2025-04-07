using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using System.Threading.Tasks;


public class npcController : MonoBehaviour
{
    static public npcController Instance;
    public Text Expediente;
    public Text local;
    public Text preguntaUno;
    public Text preguntaDos;
    public Text preguntaTres;
    public Text Respuesta;
    public npc npc;
    public dialogo dialogo;
    public float speedLocal;
    public float speedExpedient;

    private void Awake()
    {
        Instance = this;
        Instance.SetReferences();
        DontDestroyOnLoad(this.gameObject);
    }

    void SetReferences()
    {
        // if (npc == null)
        // {
        //     npc = FindFirstObjectByType<npc>();
        // }

        // if (dialogo == null)
        // {
        //     dialogo = FindObjectOfType<dialogo>();
        // }
    }

    public IEnumerator GetDataNpc(int id)
    {
        string JSONurl = $"https://localhost:7119/controller/GetNpcBasicData?id={id}";
        UnityWebRequest web = UnityWebRequest.Get(JSONurl);
        web.certificateHandler = new ForceAcceptAll();
        yield return web.SendWebRequest();

        // Deserializa los datos del NPC
        var dataofNPC = JsonConvert.DeserializeObject<npc>(web.downloadHandler.text);

        // Guarda los datos en PlayerPrefs
        PlayerPrefs.SetInt("esBueno", dataofNPC.es_bueno);
        PlayerPrefs.SetString("malaDecision", dataofNPC.textoparamaladecision);
        PlayerPrefs.SetString("local", dataofNPC.local);

        Debug.Log(JsonConvert.SerializeObject(dataofNPC, Formatting.Indented));

        if (Expediente != null)
        {
            Expediente.text = ""; // Limpia el texto antes de iniciar la animación
            StartCoroutine(textoAnimado(dataofNPC.local, local, speedLocal));
            StartCoroutine(textoAnimado(dataofNPC.expediente, Expediente, speedExpedient));
        }
        StartCoroutine(GetPreguntas(id));
    }

    public IEnumerator textoAnimado(string texto, Text component, float speed)
    {
        Debug.Log("Funcion textoAnimado casteada");
        // Debug.Log(texto);

        component.text = "";
        foreach (char character in texto.ToCharArray())
        {
            component.text += character;
            yield return new WaitForSeconds(speed);
        }
    }

    public IEnumerator GetPreguntas(int id)
    {
        Debug.Log("Get preguntas casteadas");
        preguntaUno.text ="";
        preguntaDos.text ="";
        preguntaTres.text ="";
        // Debug.Log("Get preguntas casteada");
        string JSONurl = $"https://localhost:7119/controller/GetPreguntas?id={id}";
        UnityWebRequest web = UnityWebRequest.Get(JSONurl);
        web.certificateHandler = new ForceAcceptAll();
        yield return web.SendWebRequest();

        // Deserializa la lista de preguntas
        List<preguntas> LPreguntas = JsonConvert.DeserializeObject<List<preguntas>>(web.downloadHandler.text);

        preguntaUno.text = LPreguntas[0].pregunta;
        // Debug.Log(LPreguntas[0].pregunta);
        preguntaDos.text = LPreguntas[1].pregunta;
        // Debug.Log(LPreguntas[1].pregunta);
        preguntaTres.text = LPreguntas[2].pregunta;
        // Debug.Log(LPreguntas[2].pregunta);
        StartCoroutine(GetRespuestas(id));

    }

    public IEnumerator GetRespuestas(int id)
    {
        Debug.Log("Get respuestas casteada");
        string JSONurl = $"https://localhost:7119/controller/GetRespuestas?id={id}";
        UnityWebRequest web = UnityWebRequest.Get(JSONurl);
        web.certificateHandler = new ForceAcceptAll();
        yield return web.SendWebRequest();

        // Deserializa la lista de preguntas
        List<dialogo> LRespuestas = JsonConvert.DeserializeObject<List<dialogo>>(web.downloadHandler.text);

        PlayerPrefs.SetString("RespuestaUno", LRespuestas[0].respuesta);
        PlayerPrefs.SetString("RespuestaDos", LRespuestas[1].respuesta);
        PlayerPrefs.SetString("RespuestaTres", LRespuestas[2].respuesta);

    }
}
