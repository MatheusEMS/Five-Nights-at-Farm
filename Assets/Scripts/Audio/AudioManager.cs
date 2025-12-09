using UnityEngine;
using Unity.Collections;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("------- Audio Source -------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("------- Audio Clip -------")]
    public AudioClip background;
    public AudioClip shoot;
    public AudioClip pickup;
    public AudioClip splash;
    public AudioClip efeitoCobra;
    public AudioClip victory;
    public AudioClip defeat;
    public AudioClip beep;
    //public AudioClip walkDirt;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        //se quiser que a musica comeca ao iniciar a cena
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

}
