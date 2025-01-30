using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerBuildTool : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    private RaycastHit snapHit;
    bool isHitting;
    bool isHittingSnapPoint;
    bool started;
    [SerializeField] private MeshDrawer meshDrawer;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask snapPointsLayer;
    [SerializeField] private float maxInteractionDistance;
    [SerializeField] Vector3 conveyorOffset;
    [SerializeField] ObjectPool conveyorPool;
    [SerializeField] GameObject snapPoint;
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
        snapHit = GetSnapRaycast();
        //if (isHitting)
        //{
        //    curveCalc.Calculate(hit.point);
        //}

        if (PlayerInput.Instance.GetInteractKey() && isHitting && !started)
        {
            currentConveyor = conveyorPool.GetPooledObject().GetComponent<Conveyor>();
            currentConveyor.gameObject.SetActive(true);
            currentConveyor.SetMaterial(1);
            currentConveyor.draw = true;
            started = true;
            active = true;
            startPointPlaced = false;
        }

        if (active)
        {
            //if (PlayerInput.Instance.GetInteractKey())
            //{
            //    currentConveyor.frontFaceVertices[0].position += Vector3.forward;
            //    Debug.Log(currentConveyor.frontFaceVertices[0].position);
            //}

            if (isHitting && !startPointPlaced)
            {
                if (!isHittingSnapPoint)
                {
                    currentConveyor.gameObject.transform.position = hit.point + conveyorOffset;

                }
                else
                {
                    currentConveyor.gameObject.transform.position = snapHit.collider.transform.position + Vector3.down * (currentConveyor.conveyorHeight / 2);
                    currentConveyor.gameObject.transform.rotation = snapHit.collider.transform.rotation;
                    currentConveyor.gameObject.transform.Rotate(new Vector3(0, 180, 0));
                    //hit.collider.transform.Rotate(Vector3.up * Time.deltaTime);
                    //Debug.Log(currentConveyor.gameObject.transform.eulerAngles);
                    //Debug.Log(hit.collider.transform.localPosition);

                    //if (hit.collider.transform.localEulerAngles.y != 0)
                    //{
                    //    Debug.Log(hit.collider.transform.localEulerAngles.y);
                    //}
                }
                currentConveyor.SetMaterial(1);
            }
            if (!isHitting && !startPointPlaced)
            {
                currentConveyor.gameObject.transform.position = hit.point + conveyorOffset;
                currentConveyor.SetMaterial(2);
            }
            if (!isHitting && startPointPlaced) 
            {
                currentConveyor.SetMaterial(2);
            }

            if (isHitting && startPointPlaced)
            {
                if (!isHittingSnapPoint)
                {
                    currentConveyor.transform.LookAt(hit.point);
                    currentConveyor.Calculate(hit.point);
                }
                else
                {
                    currentConveyor.transform.LookAt(new Vector3(snapHit.collider.transform.position.x, 0, snapHit.collider.transform.position.z));
                    currentConveyor.Calculate(new Vector3(snapHit.collider.transform.position.x, 0, snapHit.collider.transform.position.z));
                }
                currentConveyor.SetMaterial(1);

                
                
            }
        }

        
        

        if (PlayerInput.Instance.RightClickWasPressed() && isHitting)
        {
            if (startPointPlaced)
            {
                // place end point

                active = false;
                started = false;
                currentConveyor.SetMaterial(3);
                currentConveyor.StopDrawing();
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

    private RaycastHit GetSnapRaycast()
    {
        ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));
        isHittingSnapPoint = Physics.Raycast(ray, out RaycastHit hit, maxInteractionDistance, snapPointsLayer);

        return hit;
    }
}
