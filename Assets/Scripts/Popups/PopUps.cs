using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUps : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Slider slider;
    public string textValue;
    public float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text.text = textValue;
        slider.maxValue = timer;
        slider.minValue = 0;
    }

    private void Update()
    {
        text.text = textValue;
        slider.value = timer;
    }

}
