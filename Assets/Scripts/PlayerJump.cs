using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private PlayerMovement playerMovement;

    [SerializeField] private float jumpForce;


    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (PlayerInput.Instance.JumpWasPressed() && PlayerInput.Instance.IsGrounded())
        {
            playerMovement.playerVelocity.y = jumpForce;
        }

        
    }

    
}
