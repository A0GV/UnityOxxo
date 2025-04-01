using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

// File to solve the problems, so if the button is clicked, eliminates them from list and eliminates the obj from the list. See how to pause time while this is happening too. Like an await for either of these three to return a true. 
public class DDResolver : MonoBehaviour
{    
    public DiaControl diaControl; // Instance de controller
    public UIControlDia uiControl;

    public void SolveProblem1() 
    {
        // Eliminate problem from list
        if (diaControl.problemasActivos.Count >= 1)
        {
            diaControl.problemasActivos.RemoveAt(0);
        }
        // Problem not defined for some reason
        else 
        {
            Debug.Log("No existe ese problema");
        }

        Debug.Log("Seleccionó primera pregunta");
        uiControl.HidePregunta(); // Calls UI to handle question closing
    }

    public void SolveProblem2() 
    {
        // Eliminate problem from list
        if (diaControl.problemasActivos.Count >= 2)
        {
            diaControl.problemasActivos.RemoveAt(1);
        }
        // Problem not defined for some reason
        else 
        {
            Debug.Log("No existe ese problema");
        }

        Debug.Log("Seleccionó segunda pregunta");
        uiControl.HidePregunta(); // Calls UI to handle question closing
    }

    public void SolveProblem3() 
    {
        // Eliminate problem from list
        if (diaControl.problemasActivos.Count >= 3)
        {
            diaControl.problemasActivos.RemoveAt(2);
        }
        // Problem not defined for some reason
        else 
        {
            Debug.Log("No existe ese problema");
        }

        Debug.Log("Seleccionó tercera pregunta");
        uiControl.HidePregunta(); // Calls UI to handle question closing
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
