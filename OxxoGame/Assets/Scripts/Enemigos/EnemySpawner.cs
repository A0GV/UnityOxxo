using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemigoPrefab;  // Prefab del enemigo
    public float spawnRate = 2.0f;    // Frecuencia de aparición en segundos
    public Transform[] spawnPoints;   // Puntos desde donde aparecen los enemigos

    private Coroutine spawnCoroutine; // Referencia a la corutina de spawn

    void Start()
    {
        StartSpawner();
    }

    public void StartSpawner()
    {
        // Iniciar la corutina de generación de enemigos
        if (spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(SpawnEnemies());
        }
    }

    public void StopSpawner()
    {
        // Detener la corutina de generación de enemigos
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // Esperar el tiempo definido antes de crear otro enemigo
            yield return new WaitForSeconds(spawnRate);

            // Elegir un punto de spawn aleatorio
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Instanciar el enemigo en el punto seleccionado
            Instantiate(enemigoPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
