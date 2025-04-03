using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

// File to solve the problems, so if the button is clicked, eliminates them from list and eliminates the obj from the list. See how to pause time while this is happening too. Like an await for either of these three to return a true. 
public class DDResolver : MonoBehaviour
{    
    public DiaControl diaControl; // Instance de controller
    public UIControlDia uiControl;

    public void SolveProblem(Problema problemaResuelto)
    {
        // Por si no hay uno q se resolvió, nomas se sale de la función para no crashear todo
        if (problemaResuelto == null) {
            Debug.Log("No exsiste el problema");
            return;
        }

        // Checa si exsiste el problema
        if (diaControl.problemasActivos.Contains(problemaResuelto))
        {
            problemaResuelto.SetRenderStatus(false); // Apagarlo
            diaControl.problemasActivos.Remove(problemaResuelto); // So long bye bye
        }

        Debug.Log($"Se resolvió el problema: {problemaResuelto.GetNombreProblema()}"); // Temporary debugging
        uiControl.HidePregunta(); // UI handles question closing 
    }

    /*
    public void SolveProblem1() 
    {
        // Eliminate problem from list
        if (diaControl.problemasActivos.Count >= 1)
        {
            diaControl.problemasActivos[0].SetRenderStatus(false); // Sets as false because no longer active
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
            diaControl.problemasActivos[1].SetRenderStatus(false); // Sets as false because no longer active
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
             diaControl.problemasActivos[2].SetRenderStatus(false); // Sets as false because no longer active
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
    */

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
