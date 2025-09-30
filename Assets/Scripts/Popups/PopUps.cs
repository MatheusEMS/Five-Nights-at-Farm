using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUps : MonoBehaviour
{
    [SerializeField] private Text text;
    public string textValue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text.text = textValue;
    }


}
