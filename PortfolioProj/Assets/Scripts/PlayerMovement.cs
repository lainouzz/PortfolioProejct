using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerScriptableObject playerData;

    private CharacterController controller;
    private Vector3 Velocity;

    private float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerData.isCrouching = false;
        playerData.isRunning = false;
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

        speed = playerData.walkSpeed;
        Crouching();
        Running();
        
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDir = transform.right * moveX + transform.forward * moveZ;
        controller.Move(moveDir * (speed * Time.deltaTime));

        Velocity.y += playerData.gravity * Time.deltaTime;
        controller.Move(Velocity * Time.deltaTime);
    }

    void Crouching()
    {
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.C))
        {
            speed = playerData.crouchSpeed;
            controller.height = 0.35f;
            playerData.isCrouching = true;
        }
        else
        {
            speed = playerData.walkSpeed;
            controller.height = 2.0f;
            playerData.isCrouching = false;
        }
    }

    void Running()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = playerData.runSpeed;
            playerData.isRunning = !playerData.isRunning;
            
        }
    }
}
