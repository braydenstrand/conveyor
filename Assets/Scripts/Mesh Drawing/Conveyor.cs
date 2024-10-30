using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base Conveyor Class
/// </summary>

public class Conveyor : MonoBehaviour
{
    [SerializeField] float initialLength;

    [Header("Conveyor Dimensions")]
    [SerializeField] float width;
    [SerializeField] float height;
    [SerializeField] float postWidth;
    [SerializeField] float postHeight;

    [Header("Pole Dimensions")]
    [SerializeField] float poleWidth;
    [SerializeField] float poleHeight;
    [SerializeField] float poleDepth;

    Mesh beltMesh;
    Mesh frameMesh;

    CalculateVertexLocations vertexCalculator;
    MeshDrawer meshDrawer;

    private void Start()
    {
        beltMesh = new Mesh();
        frameMesh = new Mesh();

        vertexCalculator = GetComponent<CalculateVertexLocations>();
        vertexCalculator.Calculate(width, height, postWidth, postHeight, poleWidth, poleHeight, poleDepth);
        meshDrawer = GetComponent<MeshDrawer>();
    }
}
