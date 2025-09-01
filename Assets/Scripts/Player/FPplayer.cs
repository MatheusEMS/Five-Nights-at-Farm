using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(FPcontroller))]
public class FPplayer : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] FPcontroller fPcontroller;

    void OnMove(InputValue value)
    {
        fPcontroller.MoveInput = value.Get<Vector2>();
    }

    void OnLook(InputValue value)
    {
        fPcontroller.LookInput = value.Get<Vector2>();
    }


    void OnValidate()
    {
        if (fPcontroller == null)
        {
            fPcontroller = GetComponent<FPcontroller>();
        }
    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
