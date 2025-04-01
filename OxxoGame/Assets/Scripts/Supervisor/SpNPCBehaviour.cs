using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpNPCBehaviour : MonoBehaviour
{
    public GameObject npcgameObject;
    public float maxHeight;
    public float minHeight;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Replace KeyCode.Space with the desired key
        {
            SpawnNPC();
            Debug.Log("Ya se spawneo uno");
        }
    }

    void SpawnNPC()
    {
        Instantiate(npcgameObject,
            new Vector3(transform.position.x,
            transform.position.y +
            Random.Range(maxHeight, minHeight), 0),
            Quaternion.identity);
    }
    


}
