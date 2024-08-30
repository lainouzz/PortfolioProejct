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

    public float distanceZ;
    
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
        if (bulletScript != null)
        {
            Camera camera = Camera.main;
            Debug.Log("Calculated distanceZ: " + distanceZ);
            
            Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, distanceZ);
            Vector3 centerWorld = camera.ScreenToWorldPoint(screenCenter);
            
            Debug.Log("Screen Center: " + screenCenter);
            Debug.Log("Center World Position: " + centerWorld);
            Debug.Log("Muzzle Position: " + MuzzlePosition.position);
            
            Vector3 dirToCenter = (centerWorld - MuzzlePosition.position).normalized;  
            Debug.Log("Direction to Center: " + dirToCenter);
            
            bulletScript.initDirection(dirToCenter);
            bulletScript.rb.velocity = dirToCenter * bulletScript.bulletVelocity;
        
            bulletSpawn = true;
        }
    }
}
