using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;


public class Interactable : MonoBehaviour
{
    Outline outline;
    public string message;

    public UnityEvent onInteraction;

    //Audios
    AudioManager audioManager;

    //pega o audioManager
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        outline = GetComponent<Outline>();
        DisableOutline();
    }

    void Update()
    {

       audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();

    }

    public void Interact()
    {
        audioManager.PlaySFX(audioManager.pickup);
        onInteraction.Invoke();
    }

    public void DisableOutline()
    {
        outline.enabled = false;
    }

    public void EnableOutline()
    {
        outline.enabled = true;
    }
    /// <summary>
    /// SOMENTE PARA OS INGREDIENTES
    /// </summary>
    public void PegarIngrediente()
    {
        audioManager.PlaySFX(audioManager.pickup);
        IngredientsController.Instance.PegouIngrediente(gameObject);
    }

    public void ReciclarIngrediente()
    {
        audioManager.PlaySFX(audioManager.pickup);
        IngredientsController.Instance.reciclar();
    }


    public void AbrirTutorial()
    { 
        if (GameController.instance.CheckEstadoResultado() == false)
        {
            audioManager.PlaySFX(audioManager.pickup);
            HudController.instance.AbrirTutorial();
        }
    }
}
