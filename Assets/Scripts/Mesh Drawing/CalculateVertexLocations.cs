using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateVertexLocations : MonoBehaviour
{
    List<Vector3> vertices = new();

    PlayerBuildTool buildTool;

    Conveyor currentConveyor;

    Transform[] frontFaceVertices;
    Transform[] backFaceVertices;

    private void Start()
    {
        
    }

    public void Initialize(PlayerBuildTool playerBuildTool)
    {
        buildTool = playerBuildTool;

        GameObject parentObject = currentConveyor.frontFace;
        int childCount = parentObject.transform.childCount;
        frontFaceVertices = new Transform[childCount];

        for (int i = 0; i < childCount; i++)
        {
            frontFaceVertices[i] = parentObject.transform.GetChild(i).GetComponent<Transform>();
        }

        parentObject = currentConveyor.backFace;
        childCount = parentObject.transform.childCount;
        backFaceVertices = new Transform[childCount];

        for (int i = 0; i < childCount; i++)
        {
            backFaceVertices[i] = parentObject.transform.GetChild(i).GetComponent<Transform>();
        }
    }

    public void Calculate(Vector3 startPos, Vector3 endPos)
    {
        SetFrontFacePositions(Vector3.zero);
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
