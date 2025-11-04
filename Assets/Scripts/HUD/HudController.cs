using TMPro;
using UnityEngine;

public class HudController : MonoBehaviour
{
    public static HudController instance;

    [SerializeField] private GameObject Tutorial;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        DisableInteractionText();
    }

    [SerializeField] TMP_Text interactionText;

    public void EnableInteractionText(string text)
    {
        interactionText.text = text + " (E)";
        interactionText.enabled = true;
    }

    public void DisableInteractionText()
    {
        interactionText.enabled = false;
    }


    public void AbrirTutorial()
    {
        Tutorial.SetActive(true);
        GameController.instance.EntrouTutorial();
    }

    public void FecharTutorial()
    {
        Tutorial.SetActive(false);
    }
}
