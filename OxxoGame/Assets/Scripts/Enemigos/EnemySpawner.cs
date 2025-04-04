using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemigoPrefab;  // Prefab del enemigo
    public float spawnRate = 2.0f;    // Frecuencia de aparición en segundos
    public Transform[] spawnPoints;   // Puntos desde donde aparecen los enemigos

    void Start()
    {
        StartCoroutine(SpawnEnemies());
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
