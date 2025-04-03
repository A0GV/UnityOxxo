using UnityEngine;

public class StopAndContinueWalk : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        IndividualBehaviourNpc.instance = null;
    }
}
//NullReferenceException: Object reference not set to an instance of an object
//StopAndContinueWalk.OnTriggerEnter2D (UnityEngine.Collider2D collision) (at Assets/Scripts/Supervisor/StopAndContinueWalk.cs:21)

