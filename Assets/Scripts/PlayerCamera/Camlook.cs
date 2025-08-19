using UnityEngine;

public class Camlook : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float mouseSensi;
    private float xRot,yRot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        LookCamera();
    }

    void LookCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensi * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensi * Time.deltaTime;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -80, 80); //talvez colocar variaveis para trocar
        yRot += mouseX;

        transform.localRotation = Quaternion.Euler(xRot, yRot, 0);
        player.Rotate(Vector3.up * mouseX);
    }
}
