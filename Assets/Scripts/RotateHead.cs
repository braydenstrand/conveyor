using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHead : MonoBehaviour
{
    private float camXRotation;

    [SerializeField] private float sensitivity;
    [SerializeField] private Transform head;
    [SerializeField] private bool invertLook;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        camXRotation += PlayerInput.Instance.GetPlayerRotation().y * sensitivity * Time.deltaTime * (invertLook ? -1 : 1);
        camXRotation = Mathf.Clamp(camXRotation, -85f, 85f);

        head.localRotation = Quaternion.Euler(camXRotation, 0, 0);
    }
}
