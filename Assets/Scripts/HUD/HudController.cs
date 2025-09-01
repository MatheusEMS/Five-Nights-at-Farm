using TMPro;
using UnityEngine;

public class HudController : MonoBehaviour
{
    public static HudController instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        instance = this;
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
}
