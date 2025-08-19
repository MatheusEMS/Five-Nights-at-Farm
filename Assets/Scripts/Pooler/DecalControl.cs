using UnityEngine;

public class DecalControl : MonoBehaviour
{

    void OnEnable()
    {
        Invoke("Disable", 2f);
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }
}
