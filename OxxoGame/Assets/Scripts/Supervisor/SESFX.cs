using UnityEngine;

public class SESFX : MonoBehaviour
{
    // AudioClips para diferentes efectos de sonido
    public AudioClip buttonSound; // Sonido al presionar botones
    public AudioClip accept; // Sonido al ganar
    public AudioClip reject; // Sonido al responder medio
    
    private AudioSource audioSource;

    void Awake()
    {
        // Configuración inicial del audio
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
    }

    // Método para reproducir el sonido de botón
    public void PlayButtonSound()
    {
        audioSource.PlayOneShot(buttonSound, 0.7f);
    }

    // Método para reproducir el sonido de victoria
    public void PlayRejectound()
    {
        audioSource.PlayOneShot(reject, 1.0f);
    }

    // Método para reproducir el sonido de derrota
    public void PlayAcceptSound()
    {
        audioSource.PlayOneShot(accept, 1.0f);
    }
}