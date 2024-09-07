using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform playerTransform;
    
    [SerializeField] private float mouseXSpeed;
    [SerializeField] private float mouseYSpeed;
    [SerializeField] private float verticalRot;
    
    [SerializeField] private float leanAngle;
    [SerializeField] private float leanSpeed;
    [SerializeField] private float leanDistance;
    [SerializeField] private float leanThreshold;
    
    private float mouseX;
    private float mouseY;
    private float currentLeanAngle;
    
    private Camera camera;
    private Vector3 orignalPos;
    private Quaternion originalRot;
    
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        orignalPos = transform.localPosition;
        originalRot = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        LookX();
        LookY();
        Lean();
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

        Quaternion verticalRotation = Quaternion.Euler(this.verticalRot, 0 ,0);
        transform.localRotation = verticalRotation * Quaternion.Euler(0, 0, currentLeanAngle);
    }

    private void Lean()
    {
        float targetLeanAngle = 0;
        float targetXPos = 0;
        
        if (Input.GetKey(KeyCode.Q))
        {
            targetLeanAngle = leanAngle;
            targetXPos = -leanDistance;
        }else if (Input.GetKey(KeyCode.E))
        {
            targetLeanAngle = -leanAngle;
            targetXPos = leanDistance;
        }

        currentLeanAngle = Mathf.Lerp(currentLeanAngle, targetLeanAngle, leanSpeed * Time.deltaTime);
        Vector3 targetPos = orignalPos + new Vector3(targetXPos, 0, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, leanSpeed * Time.deltaTime);
        
        Quaternion verticalRotation = Quaternion.Euler(verticalRot, 0 ,0 );
        transform.localRotation = verticalRotation * Quaternion.Euler(0, 0, currentLeanAngle);
    }
}