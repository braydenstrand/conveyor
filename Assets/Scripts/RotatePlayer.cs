using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    

    [SerializeField] private float sensitivity;


    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(Vector3.up * PlayerInput.Instance.GetPlayerRotation().x * Time.deltaTime * sensitivity);
    }
}
