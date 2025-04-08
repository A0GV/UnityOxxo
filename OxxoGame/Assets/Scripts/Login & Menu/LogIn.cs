using UnityEngine;

public class Login
{
    public string Correo { get; set; } // Correo del usuario
    public string Contrasena { get; set; } // Contraseña del usuario
    public int? IdUsuario { get; set; } // ID del usuario obtenido tras el login

    public Login() {}

    public Login(string correo, string contrasena, int? idUsuario)
    {
        this.Correo = correo;
        this.Contrasena = contrasena;
        this.IdUsuario = idUsuario;
    }
}