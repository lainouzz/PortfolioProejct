using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Camera camera;

    [SerializeField] private float mouseXSpeed;
    [SerializeField] private float mouseYSpeed;
    [SerializeField] private float verticalRot;
    
    [SerializeField] private int leanAngle;
    [SerializeField] private float leanSpeed;
    [SerializeField] private float leanDistance;
    [SerializeField] private float leanThreshold;
    
    private float mouseX;
    private float mouseY;
    
    public Transform playerTransform;

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

        transform.localEulerAngles = Vector3.right * verticalRot;
    }

    private void Lean()
    {
        int targetLeanAngle = 0;
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
        
        Quaternion targetRot = Quaternion.Euler(0,0, targetLeanAngle);
        Vector3 targetPos = orignalPos + new Vector3(targetXPos, 0, 0);

        if (Quaternion.Angle(transform.localRotation, targetRot) < leanThreshold)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRot, leanSpeed * Time.deltaTime);
        }
       /* else
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, leanSpeed * Time.deltaTime);
        }*/

        if (Vector3.Distance(transform.localPosition, targetPos) < leanThreshold)
        {
            transform.localPosition = targetPos;
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, leanSpeed * Time.deltaTime);
        }
    }
}