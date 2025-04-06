using UnityEngine;
using System.Collections.Generic;

public class DDSpawner : MonoBehaviour
{
    public static DDSpawner Instance;
    DDPoints pointsInstance;
    public GameObject prefabAjolote;
    public Transform spawnPoint; // Donde empieza la animación
    public int maxAxolotls = 4;
    public float spawnInterval = 1.25f; // Tiempo entre spawns como float, mínimo 1.25 segudos entre spawns

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
        dd.pointMoveSpeed = CalculateSpeed();

        spawnedAxolotls.Add(newAxolotl); // Agrega ajolote a lista


    }

    // Modifies spawner speed based on how many are answered right, so more right answers makes them walk faster and respawn faster
    public float CalculateSpeed() 
    {
        int totalPointsSolved = PlayerPrefs.GetInt("countAlta", 0) + PlayerPrefs.GetInt("countMed", 0) + PlayerPrefs.GetInt("countLow", 0);  

        // Default is 3f if no questions have been answered
        if (totalPointsSolved == 0)
        {
            return 3f; 
        }
        // Otherwise at least one answered and can use that
        else 
        {
            float ratioAltas = (float)PlayerPrefs.GetInt("countAlta") / totalPointsSolved * 3; // Goes from 0 to 1, so multiplying by 3 so that max speed can be 6 with a perfect score once added to current speed
            float spawnerMoveSpeed = 3f + ratioAltas; // Base speed is 3, answering all lows will have speed at 3. If all are right, will be doing 3 + 3. If have half altas, then speed will be 4.5
            return spawnerMoveSpeed;
        }

        
    }
}
