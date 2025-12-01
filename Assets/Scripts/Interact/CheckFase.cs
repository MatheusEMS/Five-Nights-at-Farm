using Unity.VisualScripting;
using UnityEngine;

public class CheckFase : MonoBehaviour
{
    [SerializeField] private int faseParaSpawnar = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameController.instance.fase < faseParaSpawnar)
        {
            Destroy(gameObject);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
