using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerBuildTool : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    bool isHitting;
    bool started;
    [SerializeField] private MeshDrawer meshDrawer;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float maxInteractionDistance;
    [SerializeField] Vector3 conveyorOffset;
    [SerializeField] ObjectPool conveyorPool;
    Conveyor currentConveyor;

    bool startPointPlaced;
    CalculateConveyorCurve curveCalc;

    bool active;

    void Start()
    {
        curveCalc = GetComponent<CalculateConveyorCurve>();
    }

    void Update()
    {
        hit = GetRaycast();
        if (!isHitting)
        {

        }

        if (PlayerInput.Instance.GetInteractKey() && isHitting && !started)
        {
            currentConveyor = conveyorPool.GetPooledObject().GetComponent<Conveyor>();
            currentConveyor.gameObject.SetActive(true);
            currentConveyor.SetMaterial(1);
            currentConveyor.draw = true;
            started = true;
            active = true;
        }

        if (active)
        {
            if (PlayerInput.Instance.GetInteractKey())
            {
                currentConveyor.frontFaceVertices[0].position += Vector3.forward;
                Debug.Log(currentConveyor.frontFaceVertices[0].position);
            }

            if (isHitting && !startPointPlaced)
            {
                currentConveyor.gameObject.transform.position = hit.point + conveyorOffset;
                currentConveyor.SetMaterial(1);
            }
            if (!isHitting && !startPointPlaced)
            {
                currentConveyor.gameObject.transform.position = hit.point + conveyorOffset;
                currentConveyor.SetMaterial(2);
            }
            if (isHitting && startPointPlaced)
            {
                //currentConveyor.Calculate();
            }
        }

        
        

        if (PlayerInput.Instance.RightClickWasPressed() && isHitting)
        {
            if (startPointPlaced)
            {
                // place end point
            }
            else
            {
                // place start point

                startPointPlaced = true;
            }
        }
    }
    
    

    public RaycastHit GetRaycast()
    {
        ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));
        isHitting = Physics.Raycast(ray, out RaycastHit hit, maxInteractionDistance, groundLayer);

        if (isHitting)
        {
            return hit;
        }
        else
        {
            Physics.Raycast(ray, out hit, groundLayer);
            return hit;
        }
    }
}
