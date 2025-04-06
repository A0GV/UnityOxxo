using UnityEngine;

public class DDPoints : MonoBehaviour
{
    Animator animatorController;
    [SerializeField] Transform[] Points; // Porque lo puso en el tutorial
    private int pointsIndex; 
    [SerializeField] public int pointMoveSpeed; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = Points[pointsIndex].transform.position; 
    }

    // Update is called once per frame
    void Update()
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
