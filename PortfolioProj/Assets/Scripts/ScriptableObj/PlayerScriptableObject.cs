using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "myPlayerScriptableObject", menuName = "ScriptableObjects/PlayerScriptableObject")]

public class PlayerScriptableObject : ScriptableObject
{
   public float walkSpeed;
   public float crouchSpeed;
   public float runSpeed;
   
   public bool isSneaking;
   public bool isCrouching;
   public bool isRunning;
}
