using UnityEngine;
using UnityEngine.UI;

public class TextLogicsAndResults : MonoBehaviour
{
    public Text resumen;
    
     static string[] informes;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int randomIndex = Random.Range(0, informes.Length);
        resumen.text = informes[randomIndex];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
