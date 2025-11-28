using TMPro;
using UnityEngine;

public class GlobalAmmo : MonoBehaviour
{
    public static int municaopistolacount = 8;
    [SerializeField] private TextMeshProUGUI ammoDisplay;

    public static GlobalAmmo instance;

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

    // Update is called once per frame
    void Update()
    {

        ammoDisplay.text = "" + municaopistolacount;

    }
}
