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

        Debug.Log(JsonConvert.SerializeObject(dataofNPC, Formatting.Indented));

        if (Expediente != null)
        {
            Expediente.text = ""; // Limpia el texto antes de iniciar la animación
            StartCoroutine(textoAnimado(dataofNPC.local,local, speedLocal ));
            StartCoroutine(textoAnimado(dataofNPC.expediente,Expediente,speedExpedient));
        }
    }

    public IEnumerator textoAnimado(string texto, Text component, float speed)
    {
        Debug.Log("Funcion textoAnimado casteada");
        // string txt = PlayerPrefs.GetString("local");
        Debug.Log(texto);

        component.text="";
        foreach (char character in texto.ToCharArray())
        {
            Debug.Log(character);
            component.text += character;
            yield return new WaitForSeconds(speed);
        }
    }

    //  public IEnumerator localAnimado()
    // {
    //     Debug.Log("Funcion textoAnimado casteada");
    //     string txt = PlayerPrefs.GetString("expediente");
    //     Debug.Log(txt);
    //     foreach (char character in txt.ToCharArray())
    //     {
    //         Debug.Log(character);
    //         local.text += character;
    //         yield return new WaitForSeconds(speed);
    //     }
    // }
}
