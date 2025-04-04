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

            /*
            // Update el exp
            int elotePregunta = PlayerPrefs.GetInt("elotes"); 
            elotePregunta += problemaResuelto.GetPrioridad(); // Incrementa elotes por prioridad dada
            Debug.Log("Elotes nuevos: " + elotePregunta);
            PlayerPrefs.SetInt("elotes", elotePregunta); // Sets elote al valor nuevo

            int expPregunta = PlayerPrefs.GetInt("exp");
            expPregunta += problemaResuelto.GetPrioridad() * 2; 
            PlayerPrefs.SetInt("exp", expPregunta);

            PlayerPrefs.Save(); // Porque a veces no los salva
            */
            diaControl.elotesGanados += problemaResuelto.GetPrioridad();
            diaControl.expGanado += problemaResuelto.GetPrioridad() * 2;

            diaControl.problemasActivos.Remove(problemaResuelto); // So long bye bye
        }

        Debug.Log($"Se resolvió el problema: {problemaResuelto.GetNombreProblema()}"); // Temporary debugging
        uiControl.HidePregunta(); // UI handles question closing 
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
