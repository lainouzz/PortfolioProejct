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
    
    private bool bulletSpawn;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            spawnBullet();
        }
    }
    
    void spawnBullet()
    {
        GameObject newBullet = Instantiate(bulletPrefab, MuzzlePosition.position, Quaternion.identity);

        Bullet bulletScript = newBullet.GetComponent<Bullet>();
        if (bullet != null)
        {
            Camera camera = Camera.main;

            Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            Vector3 centerWorld = camera.ScreenToWorldPoint(new Vector3(screenCenter.x, screenCenter.y, MuzzlePosition.position.z));
            Vector3 dirToCenter = (centerWorld - MuzzlePosition.position).normalized;  
            
            bulletScript.initDirection(dirToCenter);
            bulletScript.rb.velocity = dirToCenter * bulletScript.bulletVelocity;
        
            bulletSpawn = true;
        }
    }
}
