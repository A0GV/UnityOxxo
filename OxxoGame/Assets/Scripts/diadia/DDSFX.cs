using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDSFX : MonoBehaviour
{
    // AudioClips para diferentes efectos de sonido
    public AudioClip buttonSound; // Sonido al presionar botones
    public AudioClip backgroundMusic; // Música de fondo
    public AudioClip highSound; // Sonido al ganar
    public AudioClip midSound; // Sonido al responder medio
    public AudioClip lowSound; // Sonido al perder
    //public AudioClip pop; // Sonido pop -21
    
    private AudioSource audioSource;

    void Awake()
    {
        // Configuración inicial del audio
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        //PlayBackgroundMusic();
    }

    // Método para reproducir el sonido de botón
    public void PlayButtonSound()
    {
        audioSource.PlayOneShot(buttonSound, 0.7f);
    }

    // Método para reproducir la música de fondo
    public void PlayBackgroundMusic()
    {
        audioSource.clip = backgroundMusic;
        audioSource.volume = 0.5f;
        audioSource.Play();
    }

    // Método para reproducir el sonido de victoria
    public void PlayHighSound()
    {
        audioSource.PlayOneShot(highSound, 1.0f);
    }

    // Método para reproducir el sonido de derrota
    public void PlayMidSound()
    {
        audioSource.PlayOneShot(midSound, 1.0f);
    }

    // Método para reproducir el sonido de derrota
    public void PlayLowSound()
    {
        audioSource.PlayOneShot(lowSound, 1.0f);
    }
}
