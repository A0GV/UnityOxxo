using UnityEngine;

public class Enemigos
{
    public int id { get; set; } // ID único de la pregunta
    public string textoPregunta { get; set; } // Texto de la pregunta
    public string[] opciones { get; set; } // Opciones de respuesta
    public int respuestaCorrecta { get; set; } // Índice de la respuesta correcta (0-3)

    public Enemigos() {}

    public Enemigos(int id, string textoPregunta, string[] opciones, int respuestaCorrecta)
    {
        this.id = id;
        this.textoPregunta = textoPregunta;
        this.opciones = opciones;
        this.respuestaCorrecta = respuestaCorrecta;
    }
}