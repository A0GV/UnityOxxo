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
            problemaResuelto.SetRenderStatus(false); // Cambiar a false para que no se render el sprite
            int prioridadResuelto = problemaResuelto.GetPrioridad(); // Store la prioridad


            // Incrementar prioridad alta 
            if (prioridadResuelto == 3) 
            {
                int currentValue = PlayerPrefs.GetInt("countAlta", 0);
                PlayerPrefs.SetInt("countAlta", currentValue + 1);
            }
            else if (prioridadResuelto == 2) 
            {
                int currentValue = PlayerPrefs.GetInt("countMed", 0);
                PlayerPrefs.SetInt("countMed", currentValue + 1);
            }
            else if (prioridadResuelto == 1)
            {
                int currentValue = PlayerPrefs.GetInt("countLow", 0);
                PlayerPrefs.SetInt("countLow", currentValue + 1);
            }
            else 
            {
                Debug.Log("Error incrementando count de prioridades");
            }

            diaControl.elotesGanados += prioridadResuelto;
            //diaControl.expGanado += prioridadResuelto * 2;
            diaControl.expGanado = (diaControl.elotesGanados + diaControl.diasJugados) * 5; // Función para exp, elotes * dias jugados multiplicado por 5

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
