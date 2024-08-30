using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [SerializeField] private float smoothSway;
    [SerializeField] private float swayMultiplier;

    [SerializeField] private float smoothLean;
    [SerializeField] private float leanMultiplier;
    // Update is called once per frame
    void Update()
    {
       MouseWeaponSway();
       WeaponLean();
    }

    private void MouseWeaponSway()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * swayMultiplier;
        float mouseY = Input.GetAxisRaw("Mouse Y") * swayMultiplier;
        
        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);
        Quaternion targetRot = rotationX * rotationY;
        
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, smoothSway * Time.deltaTime); 
    }

    private void WeaponLean()
    {
        float inputX = Input.GetAxisRaw("Horizontal") * leanMultiplier;
        
        Quaternion rotationZ = Quaternion.AngleAxis(inputX, Vector3.forward);
        Quaternion targetRot = rotationZ;
        
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, smoothLean * Time.deltaTime); 
    }
}
