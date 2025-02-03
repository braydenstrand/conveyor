using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance { get; private set; }

    private float sprintMultiplier = 3f;

    public bool isPlayerDead;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundCheckDistance;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        Actions.OnPlayerDied += PlayerDied;
    }

    private void OnDisable()
    {
        Actions.OnPlayerDied -= PlayerDied;
    }

    public void PlayerDied()
    {
        isPlayerDead = true;
        Debug.Log("Player Died");
    }

    void Start()
    {
        
    }

    public Vector2 GetMovementInput()
    {
        if (!isPlayerDead)
        {
            return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
        else
        {
            return Vector2.zero;
        }
        
        
    }

    public float GetScrollWheel()
    {
        return Input.GetAxis("Mouse ScrollWheel");
    }

    public Vector2 GetPlayerRotation()
    {
        if (!isPlayerDead)
        {
            return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }
        else
        {
            return Vector2.zero;
        }
        
    }

    public float GetMoveMultiplier()
    {
        return Input.GetKey(KeyCode.LeftShift) ? sprintMultiplier : 1f;
    }

    public bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);
    }

    public bool JumpWasPressed()
    {
        if (!isPlayerDead)
        {
            return Input.GetButtonDown("Jump");
        }
        else
        {
            return false;
        }
        
    }

    public bool ShootWasPressed()
    {
        if (!isPlayerDead)
        {
            return Input.GetButtonDown("Fire1");
        }
        else
        {
            return false;
        }
        
    }

    public bool RightClickWasPressed()
    {
        if (!isPlayerDead)
        {
            return Input.GetButtonDown("Fire2");
        }
        else
        {
            return false;
        }

    }

    public bool Weapon1Pressed()
    {
        if (!isPlayerDead)
        {
            return Input.GetKeyDown(KeyCode.Alpha1);
        }
        else
        {
            return false;
        }
        
    }
    
    public bool Weapon2Pressed()
    {
        if (!isPlayerDead)
        {
            return Input.GetKeyDown(KeyCode.Alpha2);
        }
        else
        {
            return false;
        }
        
    }

    public bool GetInteractKey()
    {
        if (!isPlayerDead)
        {
            return Input.GetKeyDown(KeyCode.E);
        }
        else
        {
            return false;
        }
        
    }

    public bool HasCommanded()
    {
        return Input.GetKeyDown(KeyCode.F);
    }
}
