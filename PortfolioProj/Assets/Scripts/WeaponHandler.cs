using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public Bullet bullet;
    public Rigidbody rb;
    public GameObject bulletPrefab;
    public Transform MuzzlePosition;
    public WeaponScriptableObject weaponSO;
    public Animator anim;
    
    [SerializeField] private float distanceZ;
    
    private bool bulletSpawn;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    private void Start()
    {
        weaponSO.ammo = weaponSO.maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && weaponSO.ammo > 0)
        {
            anim.SetTrigger("IsShooting");
            spawnBullet();
        }
        else if(weaponSO.ammo <= 0)
        {
            anim.SetBool("NoBullet", true);
            weaponSO.ammo = 0;
        }
        
        Reload();
    }
    
    void spawnBullet()
    {
        GameObject newBullet = Instantiate(bulletPrefab, MuzzlePosition.position, Quaternion.identity);

        Bullet bulletScript = newBullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            Camera camera = Camera.main;
            
            Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, distanceZ);
            Vector3 centerWorld = camera.ScreenToWorldPoint(screenCenter);
            Vector3 dirToCenter = (centerWorld - MuzzlePosition.position).normalized;  
            
            bulletScript.initDirection(dirToCenter);
            bulletScript.rb.velocity = dirToCenter * bulletScript.bulletVelocity;
        
            bulletSpawn = true;
        }
        weaponSO.ammo--;
    }

    void Reload()
    {
        if (weaponSO.ammo <= 0 || weaponSO.ammo >= 0)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                anim.SetTrigger("IsReloading");
                weaponSO.ammo = weaponSO.maxAmmo;
            }
        }
    }
}
