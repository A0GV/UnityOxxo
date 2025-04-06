using UnityEngine;
using System.Collections.Generic;

public class DDSpawner : MonoBehaviour
{
    public static DDSpawner Instance;
    public GameObject prefabAjolote;
    public Transform spawnPoint; // Donde empieza la animación
    public int maxAxolotls = 4;
    public float spawnInterval = 3f; // Tiempo entre spawns como float

    private List<GameObject> spawnedAxolotls = new List<GameObject>();
    private float timerSpawn; // Otro timer solo para sus movimeintos

    public Transform[] SpawnerPoints;

    void Awake()
    {
        Instance = this;
    }

    public void StartSpawning()
    {
        // Borra los ajolotes del día previo
        for (int i = 0; i < spawnedAxolotls.Count; i++)
        {
            // Asegura q si exsiste y lo borra
            if (spawnedAxolotls[i] != null)
                Destroy(spawnedAxolotls[i]);
        }
        spawnedAxolotls.Clear(); // Borra elems de lista 

        SpawnAxolotl(); // Crea el primero
        timerSpawn = 0f;
    }

    void Update()
    {
        // If day is active, spawns new axolotls
        if (DiaControl.Instance.checkDayActive)
        {
            timerSpawn += Time.deltaTime;

            // Checks if still missing to create more and enough time has passed since last one
            if (spawnedAxolotls.Count < maxAxolotls && timerSpawn >= spawnInterval)
            {
                SpawnAxolotl();
                timerSpawn = 0f; // Resets timer to spawn next one
            }
        }

        
    }

    // Agrega otro ajolote
    void SpawnAxolotl()
    {
        // Usa prefab para crear un axolotl en el spawn point y idk q es Quaternion pero así está en el de Nina
        GameObject newAxolotl = Instantiate(prefabAjolote, spawnPoint.position, Quaternion.identity);

        DDPoints dd = newAxolotl.GetComponent<DDPoints>(); // Guarda points aquí 
        dd.SendMessage("SetPath", SpawnerPoints); // Llama para set los points desde el otro script

        spawnedAxolotls.Add(newAxolotl); // Agrega ajolote a lista


    }
}
