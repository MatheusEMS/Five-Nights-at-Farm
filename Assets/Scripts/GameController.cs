using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance { get; private set; }

    void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
