using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerBuildTool : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    bool isHitting;
    [SerializeField] private MeshDrawer meshDrawer;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float maxInteractionDistance;
    CalculateConveyorCurve curveCalc;

    void Start()
    {
        curveCalc = GetComponent<CalculateConveyorCurve>();
    }

    void Update()
    {
        RaycastHit hit = GetRaycast();

        if (PlayerInput.Instance.GetInteractKey() && isHitting)
        {
            meshDrawer.draw = true;
        }

        if (isHitting && hit.point.z > 0)
        {
            meshDrawer.length = hit.point.z;
            
        }

        if (PlayerInput.Instance.RightClickWasPressed() && isHitting)
        {
            if (curveCalc.startCubePlaced)
            {
                curveCalc.Calculate(hit.point);
            }
            else
            {
                curveCalc.PlaceFirstCube(hit.point);
            }
        }
    }
    
    RaycastHit GetRaycast()
    {
        ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));
        isHitting = Physics.Raycast(ray, out hit, maxInteractionDistance, groundLayer);

        return hit;
    }

    public Vector3 GetRaycastHitPoint()
    {
        if (isHitting)
        {
            return hit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }
}
