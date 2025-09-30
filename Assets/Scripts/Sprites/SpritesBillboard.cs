using UnityEngine;

public class SpritesBillboard : MonoBehaviour
{
    [SerializeField] bool freezeXZaxis = true;

    // Update is called once per frame
    private void LateUpdate()
    {
        if (freezeXZaxis)
        {
            transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
        }
        else
        {
            transform.rotation = Camera.main.transform.rotation;
        }
    }
       
}
