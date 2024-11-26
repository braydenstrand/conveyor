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

    public void Calculate()
    {
        RaycastHit raycastHit = buildTool.GetRaycast();
        Vector3 offset = raycastHit.point - currentConveyor.frontFacePoint.position;
        Debug.Log(offset);
        for (int i = 0; i < currentConveyor.frontFaceVertices.Count; i++)
        {
            currentConveyor.frontFaceVertices[i].position += offset;
            //Debug.Log(currentConveyor.frontFaceVertices[i]);
        }
        currentConveyor.frontFacePoint.position += offset;
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
