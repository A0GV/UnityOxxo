using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    // AudioClips para diferentes efectos de sonido
    public AudioClip buttonSound;    // Sonido al presionar botones
    public AudioClip backgroundMusic; // Música de fondo
    public AudioClip winSound;       // Sonido al ganar
    public AudioClip loseSound;      // Sonido al perder
    public AudioClip midSound;      // Sonido al perder

    private AudioSource audioSource;

    void Awake()
    {
        // Configuración inicial del audio
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        PlayBackgroundMusic();
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
    public void PlayWinSound()
    {
        audioSource.PlayOneShot(winSound, 1.0f);
    }

    // Método para reproducir el sonido de derrota
    public void PlayLoseSound()
    {
        audioSource.PlayOneShot(loseSound, 1.0f);
    }

    // Método para reproducir el sonido de derrota
    public void PlayMidSound()
    {
        audioSource.PlayOneShot(midSound, 1.0f);
    }
}
