using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "myAIScriptableObject", menuName = "ScriptableObjects/AIScriptableObject")]

public class AIScriptableObject : ScriptableObject
{
   public float walkSpeed;
   public float runSpeed;

   public float currentHealth;
   public float maxHealth;
   
   public float fieldOfView;
   public float detectRange;
   
   public bool isDead;
   public bool canSeeingPlayer;
   public bool isPatrolling;
   public bool isIdling;
   public bool isChasingPlayer;
}
