using UnityEngine;

public class GlobalAmmo : MonoBehaviour
{
    public static int municaopistolacount = 8;
    [SerializeField] GameObject ammoDisplay;




    // Update is called once per frame
    void Update()
    {

        ammoDisplay.GetComponent<TMPro.TMP_Text>().text = "" + municaopistolacount;


    }
}
