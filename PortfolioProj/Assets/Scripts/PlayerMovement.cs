using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerScriptableObject playerData;

    private CharacterController controller;
    private Vector3 Velocity;

    
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Movemement();
    }

    void Movemement()
    {
        playerData.isGrounded = controller.isGrounded;

        if (playerData.isGrounded && Velocity.y < 0)
        {
            Velocity.y = -2f;
        }

        float speed = playerData.walkSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = playerData.runSpeed;
            playerData.isRunning = true;
            
        }else if (Input.GetKey(KeyCode.LeftControl))
        {
            speed = playerData.crouchSpeed;
            playerData.isCrouching = true;
            
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDir = transform.right * moveX + transform.forward * moveZ;
        controller.Move(moveDir * (speed * Time.deltaTime));

        Velocity.y = playerData.gravity * Time.deltaTime;
        controller.Move(Velocity * Time.deltaTime);
    }
}
