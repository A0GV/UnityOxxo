using UnityEngine;
using UnityEngine.UI;

public class AxolotlSpawner : MonoBehaviour
{
    public GameObject[] axolotlPrefabs; // Lista de axolotes disponibles
    public Transform spawnPoint; // Punto donde aparecerá el axolote
    public Button spawnButton1; // Primer botón
    public Button spawnButton2; // Segundo botón
    private GameObject currentAxolotl; // Referencia al axolote actual

    public UIControlSuper uIControlSuper;

    private int contador = 0;

    private void Start()
    {

    }

    void Update()
    {
        if (currentAxolotl == null)
        {
            SpawnAxolotl();
        }

        if (contador == 7)
        {
            uIControlSuper.ShowResumen();
        }
    }

    public void SpawnAxolotl()
    {
        // Aqui mi idea es que, cuando se haga spawn de uno, llame a la funcion que jala los datos, de ahi, los guarda en pleyer prefs,
        // DEspues, cuando ya se pulse el boton. Poder saber si presiono Acept o deny, en base esto, comparar con lo que llego de API
        // Y guardarlo en otro player prefs, que se enseñara al final, despues y tendra la opcion de ver en que se equivoco y porque.

        
        contador++;
        // Si ya hay un Axolotl en la escena, lo eliminamos

        // Selecciona aleatoriamente un prefab de axolote
        GameObject selectedAxolotl = axolotlPrefabs[Random.Range(0, axolotlPrefabs.Length)];

        // Instancia el nuevo Axolotl en la posición del spawnPoint
        currentAxolotl = Instantiate(selectedAxolotl, spawnPoint.position, Quaternion.identity);
    }
}
