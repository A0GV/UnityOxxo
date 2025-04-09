using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigosSFX : MonoBehaviour
{
    public static EnemigosSFX Instance; // Singleton para acceder desde otros scripts

    [Header("Clips de Sonido")]
    public AudioClip buttonClickClip;         // Sonido para clicks de botón
    public AudioClip tutorialMusicClip;       // Música de fondo del tutorial
    public AudioClip gameMusicClip;           // Música de fondo del juego
    public AudioClip correctAnswerClip;       // Sonido para respuesta correcta o racha
    public AudioClip wrongAnswerClip;         // Sonido para respuesta incorrecta o tiempo agotado
    public AudioClip shootClip;               // Sonido al disparar una bala
    public AudioClip rachaEffectClip;         // Sonido especial para cuando estás en racha
    public AudioClip enemyHitPlayerClip;      // Sonido cuando el enemigo toca al jugador

    private AudioSource audioSource;

    void Awake()
    {
        // Configuración inicial del audio
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false; // Por defecto, los efectos de sonido no se repiten
    }

    public void PlayButtonClick()
    {
        audioSource.PlayOneShot(buttonClickClip, 0.7f);
    }

    public void PlayTutorialMusic()
    {
        audioSource.clip = tutorialMusicClip;
        audioSource.volume = 0.5f;
        audioSource.Play();
    }

    public void PlayGameMusic()
    {
        audioSource.clip = gameMusicClip;
        audioSource.volume = 0.5f;
        audioSource.Play();
    }

    public void PauseGameMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }

    public void ResumeGameMusic()
    {
        if (!audioSource.isPlaying && audioSource.clip == gameMusicClip)
        {
            audioSource.UnPause();
        }
    }

    public void PlayCorrectAnswer()
    {
        audioSource.PlayOneShot(correctAnswerClip, 1.0f);
    }

    public void PlayWrongAnswer()
    {
        audioSource.PlayOneShot(wrongAnswerClip, 1.0f);
    }

    public void PlayShoot()
    {
        audioSource.PlayOneShot(shootClip, 1.0f);
    }

    public void PlayRachaEffect()
    {
        audioSource.PlayOneShot(rachaEffectClip, 1.0f);
    }

    public void PlayEnemyHitPlayer()
    {
        audioSource.PlayOneShot(enemyHitPlayerClip, 1.0f);
    }

    public void StopMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    private void PlayMusic(AudioClip clip, float volume, bool loop)
    {
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.loop = loop;
        audioSource.Play();
    }
}
