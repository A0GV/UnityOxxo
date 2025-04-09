using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AxolotlSpawner : MonoBehaviour
{
    public GameObject[] axolotlPrefabs; // Lista de axolotes disponibles
    public Transform spawnPoint; // Punto donde aparecerá el axolote
    public Button spawnButton1; // Primer botón
    public Button spawnButton2; // Segundo botón
    private GameObject currentAxolotl; // Referencia al axolote actual

    public UIControlSuper uIControlSuper;

    public npcController npcController;

    private int contador = 0;
    private int flag;

    private List<int> availableNpcIds = new List<int>(); // List to track available NPC IDs

    private bool resumenMostrado = false; // Variable para controlar si ya se mostró el resumen

    private void Start()
    {
        // Initialize the list with all NPC IDs
        availableNpcIds = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
    }

    void Update()
    {
        if (currentAxolotl == null)
        {
            SpawnAxolotl();
        }

        if (contador == 7 && !resumenMostrado)
        {
            resumenMostrado = true; // Evita que se llame repetidamente

            if (uIControlSuper.resumenExtendido.activeSelf && flag == 0)
            {
                flag = 1;
                uIControlSuper.ShowResumenExtendidio();
            }
            else
            {
                uIControlSuper.ShowResumen();
            }
        }
    }

    public void SpawnAxolotl()
    {
        // Debug.Log("Spawner casteado");

        int randomIndex = Random.Range(0, availableNpcIds.Count);
        int selectedNpcId = availableNpcIds[randomIndex];

        availableNpcIds.RemoveAt(randomIndex);

        var data = npcController.StartCoroutine(npcController.GetDataNpc(selectedNpcId));

        contador++;

        // Selecciona aleatoriamente un prefab de axolote
        GameObject selectedAxolotl = axolotlPrefabs[Random.Range(0, axolotlPrefabs.Length)];

        uIControlSuper.ShowMain();
        // Instancia el nuevo Axolotl en la posición del spawnPoint
        currentAxolotl = Instantiate(selectedAxolotl, spawnPoint.position, Quaternion.identity);
    }
}
