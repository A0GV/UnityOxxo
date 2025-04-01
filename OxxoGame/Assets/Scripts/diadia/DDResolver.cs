using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// File to solve the problems, so if the button is clicked, eliminates them from list and eliminates the obj from the list. See how to pause time while this is happening too. Like an await for either of these three to return a true. 
public class DDResolver : MonoBehaviour
{    
    public DiaControl diaController; // Instancia de DiaControl
    public UIControlDia uiController; // Instancia de UI 

    public bool SolveProblem1() 
    {
        bool solved1 = false; 

        // Eliminate problem from list

        solved1 = true; 
        return solved1;
    }

    public bool SolveProblem2() 
    {
        bool solved2 = false; 

        solved2 = true; 
        return solved2;
    }

    public bool SolveProblem3() 
    {
        bool solved3 = false; 

        solved3 = true; 
        return solved3;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
