using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapPoint : MonoBehaviour
{
    [SerializeField] SphereCollider sphereCollider;
    [SerializeField] bool isBackSnapPoint;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void DisableCollider()
    {
        sphereCollider.enabled = false;
    }

    public void EnableCollider()
    {
        sphereCollider.enabled=true;
    }
}
