using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "myPlayerScriptableObject", menuName = "ScriptableObjects/PlayerScriptableObject")]

public class PlayerScriptableObject : ScriptableObject
{
   public float walkSpeed;
   public float crouchSpeed;
   public float runSpeed;
   public float gravity;
   
   public bool isSneaking = false;
   public bool isCrouching = false;
   public bool isRunning = false;
   public bool isGrounded;
}
