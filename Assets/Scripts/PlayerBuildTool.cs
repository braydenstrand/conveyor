using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildTool : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    bool isHitting;
    [SerializeField] private MeshDrawer meshDrawer;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float maxInteractionDistance;

    void Start()
    {
        
    }

    void Update()
    {
        GetRaycast();

        if (PlayerInput.Instance.GetInteractKey() && isHitting)
        {
            meshDrawer.InitializeBuidTool();
        }

        if (PlayerInput.Instance.ShootWasPressed())
        {
            
        }
    }
    
    void GetRaycast()
    {
        ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));
        isHitting = Physics.Raycast(ray, out hit, maxInteractionDistance, groundLayer);
    }
}
