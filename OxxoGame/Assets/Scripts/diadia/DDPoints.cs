using UnityEngine;

public class DDPoints : MonoBehaviour
{
    // Local a este archivo
    Animator animatorController;
    [SerializeField] Transform[] Points; // Porque lo puso en el tutorial
    private int pointsIndex; 
    [SerializeField] private int pointMoveSpeed = 3 ; // Sets start at 3, gets faster if user has more right

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = Points[pointsIndex].transform.position; 
    }
    
    // Para acceder puntos desde Spawner script
    public void SetPath(Transform[] pathPoints)
    {
        Points = pathPoints;
        pointsIndex = 0;

        if (Points != null && Points.Length > 0)
        {
            transform.position = Points[pointsIndex].position;
        }
    }

    // Modifies spawner speed based on satisfaction
    public void calculateSpeed() 
    {
        int totalPointsSolved = PlayerPrefs.GetInt("countAlta", 0) + PlayerPrefs.GetInt("countMed", 0) + PlayerPrefs.GetInt("countLow", 0);
    }

    // Update is called once per frame
    void Update()
    {
        // Only updates walking if day is active
        if (DiaControl.Instance.checkDayActive) 
        {
            // Moves to next position using speed and time
            if (pointsIndex <= Points.Length - 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, Points[pointsIndex].transform.position, pointMoveSpeed * Time.deltaTime); 

                // Goes to next point
                if (transform.position == Points[pointsIndex].transform.position)
                {
                    pointsIndex++; 
                }

                // Restarts the walk
                if (pointsIndex == Points.Length)
                {
                    pointsIndex = 0;
                }
            }
        }
    }
}
