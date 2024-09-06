using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimationHandler : MonoBehaviour
{
    private WeaponHandler weaponHandler;
    
    // Start is called before the first frame update
    void Start()
    {
        weaponHandler = GetComponentInParent<WeaponHandler>();
    }

    public void OnShootAnimationEvent()
    {
        if (weaponHandler != null)
        {
            weaponHandler.Shoot();
        }
    }
}
