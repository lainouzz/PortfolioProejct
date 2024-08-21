using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHealth : MonoBehaviour
{
    public AIScriptableObject aISO;
    public GameObject aiPrefab;
    
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        aISO.currentHealth = aISO.maxHealth;
    }

    public void takeDamage(float damage)
    {
        aISO.currentHealth -= damage;
        Debug.Log("enemy took " + damage + " damage");
        
        if (aISO.currentHealth <= 0)
        {
            aISO.currentHealth = 0;
            DestroyEnemy();
        }
    }

    void DestroyEnemy()
    {
        Destroy(aiPrefab);
    }
}
