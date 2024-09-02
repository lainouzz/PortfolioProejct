using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float maxDistance;
    
    public float bulletVelocity;
    
    public LayerMask HitLayer;
    public Rigidbody rb;
    public WeaponScriptableObject WeaponSO;
    
    private Vector3 dir;
    private AIHealth aiHealth;
    private bool hasHit;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(DestroyBullet), 5f);
    }

    public void initDirection(Vector3 direction)
    {
        dir = direction.normalized;
        transform.rotation = Quaternion.LookRotation(dir);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!hasHit)
        {
            RaycastHit hit;
            
            float distanceTraveled = bulletVelocity * Time.deltaTime;
            rb.velocity = dir * bulletVelocity;
            
            transform.rotation = Quaternion.LookRotation(dir);
            if (Physics.Raycast(transform.position,dir, out hit, distanceTraveled, HitLayer))
            {
                bulletHit(hit);
            }
        }
    }
    
    void bulletHit(RaycastHit hit)
    {
        AIHealth hitAiHealth = hit.collider.GetComponent<AIHealth>();
        if (hitAiHealth != null)
        {
            hitAiHealth.takeDamage(WeaponSO.damage);
        }
        hasHit = true;
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
