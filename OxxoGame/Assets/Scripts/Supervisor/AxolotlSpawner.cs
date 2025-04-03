using UnityEngine;
using UnityEngine.UI;

public class AxolotlSpawner : MonoBehaviour
{
    public GameObject[] axolotlPrefabs; // Lista de axolotes disponibles
    public Transform spawnPoint; // Punto donde aparecerá el axolote
    public Button spawnButton1; // Primer botón
    public Button spawnButton2; // Segundo botón
    private GameObject currentAxolotl; // Referencia al axolote actual

    private void Start()
    {

    }

    void Update()
    {
        if (currentAxolotl == null)
        {
            SpawnAxolotl();
        }
    }

    public void SpawnAxolotl()
    {
        // Si ya hay un Axolotl en la escena, lo eliminamos

        // Selecciona aleatoriamente un prefab de axolote
        GameObject selectedAxolotl = axolotlPrefabs[Random.Range(0, axolotlPrefabs.Length)];

        // Instancia el nuevo Axolotl en la posición del spawnPoint
        currentAxolotl = Instantiate(selectedAxolotl, spawnPoint.position, Quaternion.identity);
    }
}
