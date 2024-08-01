using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "myAIScriptableObject", menuName = "ScriptableObjects/AIScriptableObject")]

public class AIScriptableObject : ScriptableObject
{
   public float walkSpeed;
   public float runSpeed;

   public bool hasSeenPlayer;
   public bool isPatrolling;
   public bool isIdling;
   public bool isChasingPlayer;
}
