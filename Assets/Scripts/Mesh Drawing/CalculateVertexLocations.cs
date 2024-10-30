using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateVertexLocations : MonoBehaviour
{
    List<Vector3> vertices = new();

    PlayerBuildTool buildTool;

    Vector3 startingPos;

    private void Start()
    {
        buildTool = FindObjectOfType<PlayerBuildTool>();
    }

    public void Calculate(float width, float height, float postWidth, float postHeight, float poleWidth, float poleHeight, float poleDepth)
    {
        startingPos = buildTool.GetRaycastHitPoint();
    }

    void CalculateCap(float width, float height, float postWidth, float postHeight)
    {

    }
}
