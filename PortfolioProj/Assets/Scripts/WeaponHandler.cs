using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public Bullet bullet;
    
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform MuzzlePosition;
    [SerializeField] private WeaponScriptableObject weaponSO;
    [SerializeField] private Animator anim;

    [SerializeField] private float distanceZ;
    [SerializeField] private float fireRate;
    
    private Camera camera;
    
    private float timeSinceLastShot;
    
    private bool bulletSpawn;
    private bool canReload;
    private bool canReloadNewBullet;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    private void Start()
    {
        weaponSO.ammo = weaponSO.maxAmmo;
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Mouse0) && timeSinceLastShot >= fireRate)
        {
            anim.SetTrigger("IsShooting");
            timeSinceLastShot = 0;
        }
        Reload();
        ReloadNewBullet();
    }

    public void Shoot()
    {
        if (weaponSO.ammo > 0)
        {
            spawnBullet();
        }
        else
        {
            anim.SetBool("NoBullet", true);
            weaponSO.ammo = 0;
            weaponSO.isOutOfAmmo = true;
            canReloadNewBullet = true;
        }
    }
    
    public void spawnBullet()
    {
        GameObject newBullet = Instantiate(bulletPrefab, MuzzlePosition.position, Quaternion.identity);

        Bullet bulletScript = newBullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            Vector3 screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, camera.nearClipPlane +  distanceZ);
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
        if (weaponSO.ammo >= 1 && weaponSO.ammo < weaponSO.maxAmmo )
        {
            canReload = true;
            canReloadNewBullet = false;
            if (Input.GetKeyDown(KeyCode.R))
            {
                weaponSO.isReloading = true;
                anim.SetTrigger("IsReloading");
                weaponSO.ammo = weaponSO.maxAmmo;
            } 
        }
        else
        {
            canReload = false;
        }
    }

    void ReloadNewBullet()
    {
        if (weaponSO.isOutOfAmmo && canReloadNewBullet)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                weaponSO.ammo = weaponSO.maxAmmo;
                anim.SetTrigger("NewBullet");
                anim.SetBool("NoBullet", false);
            }
        }

    }
}
