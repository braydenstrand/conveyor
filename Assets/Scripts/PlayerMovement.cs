using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    private CharacterController characterController;

    [SerializeField] private float speed = 5f;

    public Vector3 playerVelocity;
    private float gravity = -20f;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        characterController.Move((transform.forward * PlayerInput.Instance.GetMovementInput().y + transform.right * PlayerInput.Instance.GetMovementInput().x).normalized * Time.deltaTime * speed * PlayerInput.Instance.GetMoveMultiplier());

        if (PlayerInput.Instance.IsGrounded() && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        playerVelocity.y += gravity * Time.deltaTime;

        characterController.Move(playerVelocity * Time.deltaTime);
    }
}
