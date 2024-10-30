using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base Conveyor Class
/// </summary>

public class Conveyor : MonoBehaviour
{
    GameObject frontFace;
    GameObject backFace;

    List<QuadVectors> frontFaceQuads = new();
    List<QuadVectors> backFaceQuads = new();
    List<QuadVectors> otherQuads = new();

    Mesh beltMesh;
    Mesh frameMesh;

    CalculateVertexLocations vertexCalculator;
    MeshDrawer meshDrawer;
    MeshTemplate template;
    PlayerBuildTool playerBuildTool;

    private void Start()
    {
        beltMesh = new Mesh();
        frameMesh = new Mesh();

        vertexCalculator = GetComponent<CalculateVertexLocations>();
        meshDrawer = GetComponent<MeshDrawer>();
        template = GetComponent<MeshTemplate>();
        meshDrawer.template = template;
        playerBuildTool = GetComponent<PlayerBuildTool>();
    }

    private void Update()
    {
       // vertexCalculator.Calculate(playerBuildTool.GetRaycastHitPoint)
    }
}
