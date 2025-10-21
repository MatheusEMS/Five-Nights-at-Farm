using UnityEngine;
using Unity.Collections;

public class AudioManager : MonoBehaviour
{
    [Header("------- Audio Source -------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("------- Audio Clip -------")]
    public AudioClip background;
    public AudioClip shoot;
    public AudioClip pickup;
    //public AudioClip walkDirt;


    private void Start()
    {
        //se quiser que a musica comeca ao iniciar a cena
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

}
