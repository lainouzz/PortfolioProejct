using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "myWeaponScriptableObject", menuName = "ScriptableObjects/Weapon")]

public class WeaponScriptableObject : ScriptableObject
{
    public float damage;

    public int ammo;
    public int maxAmmo;

    public bool isReloading;
    public bool isShooting;
    public bool isOutOfAmmo;
}
