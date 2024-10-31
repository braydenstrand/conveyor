using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateVertexLocations : MonoBehaviour
{
    List<Vector3> vertices = new();

    PlayerBuildTool buildTool;

    Conveyor currentConveyor;

    private void Start()
    {
        buildTool = FindObjectOfType<PlayerBuildTool>();
    }

    public void Calculate(Vector3 startPos, Vector3 endPos)
    {
        SetFrontFacePositions(Vector3.zero);
    }

    void SetFrontFacePositions(Vector3 translation)
    {
        Transform[] vertices = currentConveyor.frontFace.GetComponentsInChildren<Transform>();
        Debug.Log(vertices.Length);
    }

    void SetBackFacePositions(Vector3 translation)
    {

    }

    

    public void SetCurrentConveyor(Conveyor conveyor)
    {
        currentConveyor = conveyor;
    }
}
