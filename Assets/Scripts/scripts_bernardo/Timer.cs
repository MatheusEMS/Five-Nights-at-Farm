using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TimerText;
    [SerializeField] float remainingTime;
    // Update is called once per frame
    void Update()
    {
        if (remainingTime > 0)
        {

            remainingTime -= Time.deltaTime;

        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;

        }
       
        int minutos = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        TimerText.text = string.Format("{0:00}:{1:00}", minutos, seconds);
    }
}
