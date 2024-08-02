using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Camera camera;

    [SerializeField]private float mouseXSpeed;
    [SerializeField]private float mouseYSpeed;
    [SerializeField] private float verticalRot;
    
    private float mouseX;
    private float mouseY;
    
    public Transform playerTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        LookX();
        LookY();
    }

    private void LookX()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseXSpeed;

        playerTransform.Rotate(Vector3.up * mouseX);
    }
    
    private void LookY()
    {
        mouseY = Input.GetAxis("Mouse Y") * mouseYSpeed;
        verticalRot -= mouseY;
        verticalRot = Mathf.Clamp(verticalRot, -89, 89);

        transform.localEulerAngles = Vector3.right * verticalRot;
    }
}
