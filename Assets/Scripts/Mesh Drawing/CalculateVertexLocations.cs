using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateVertexLocations : MonoBehaviour
{
    List<Vector3> vertices = new();

    PlayerBuildTool buildTool;

    Conveyor currentConveyor;


    private void Awake()
    {
        
    }

    public void TestCalculate()
    {
        RaycastHit raycastHit = buildTool.GetRaycast();
        Vector3 offset = raycastHit.point - currentConveyor.backFacePoint.position;
        currentConveyor.backFaceVertices[0].position += offset;
        currentConveyor.backFacePoint.position += offset;

        Debug.Log(currentConveyor.backFacePoint.position);
        Debug.Log(raycastHit.point);
        Debug.Log(currentConveyor.backFaceVertices[0].position);
    }

    public void Calculate(Vector3 endPoint)
    {
        Vector3 offset = endPoint - currentConveyor.backFacePoint.position;
        
        for (int i = 0; i < currentConveyor.backFaceVertices.Count; i++)
        {
            currentConveyor.backFaceVertices[i].position += offset;
            //Debug.Log(currentConveyor.frontFaceVertices[i]);
        }
        currentConveyor.backFacePoint.position += offset;

        
    }

    public void Initialize(PlayerBuildTool playerBuildTool, Conveyor conveyor)
    {
        buildTool = playerBuildTool;
        currentConveyor = conveyor;

        //GameObject parentObject = currentConveyor.frontFace;
        //int childCount = parentObject.transform.childCount;
        //frontFaceVertices = new Transform[childCount];

        //for (int i = 0; i < childCount; i++)
        //{
        //    frontFaceVertices[i] = parentObject.transform.GetChild(i).GetComponent<Transform>();
        //}

        //parentObject = currentConveyor.backFace;
        //childCount = parentObject.transform.childCount;
        //backFaceVertices = new Transform[childCount];

        //for (int i = 0; i < childCount; i++)
        //{
        //    backFaceVertices[i] = parentObject.transform.GetChild(i).GetComponent<Transform>();
        //}


        // New code

        

        //foreach (Transform face in currentConveyor.faces)
        //{
        //    childCount = face.childCount;
        //    for(int i = 0;i < childCount; i++)
        //    {
        //        child = face.GetChild(i);
        //        if (child.gameObject.CompareTag("Quad"))
        //        {
        //            //currentConveyor.quads.Add(child);
        //        }
        //    }
        //}
    }

    void InitializeFaces()
    {

    }

    

    void SetFrontFacePositions(Vector3 translation)
    {
        
    }

    void SetBackFacePositions(Vector3 translation)
    {

    }

    

    public void SetCurrentConveyor(Conveyor conveyor)
    {
        currentConveyor = conveyor;
    }
}
