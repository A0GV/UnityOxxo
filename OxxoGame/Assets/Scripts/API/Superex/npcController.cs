using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;


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
        var dataofNPC = JsonConvert.DeserializeObject<npc>(web.downloadHandler.text);
        Debug.Log(JsonConvert.SerializeObject(dataofNPC, Formatting.Indented));
    
    }


}
