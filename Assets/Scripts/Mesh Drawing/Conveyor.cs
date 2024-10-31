using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base Conveyor Class
/// </summary>

public class Conveyor : MonoBehaviour
{
    [HideInInspector] public GameObject frontFace;
    [HideInInspector] public GameObject backFace;

    [HideInInspector] public List<QuadVectors> frontFaceQuads = new();
    [HideInInspector] public List<QuadVectors> backFaceQuads = new();
    [HideInInspector] public List<QuadVectors> otherQuads = new();

    Mesh beltMesh;
    Mesh frameMesh;

    CalculateVertexLocations vertexCalculator;
    MeshDrawer meshDrawer;
    MeshTemplate template;
    PlayerBuildTool playerBuildTool;

    [SerializeField] GameObject conveyorTemplateObject;

    private void Start()
    {
        beltMesh = new Mesh();
        frameMesh = new Mesh();

        playerBuildTool = FindObjectOfType<PlayerBuildTool>();
        template = conveyorTemplateObject.GetComponent<MeshTemplate>();
        meshDrawer = GetComponent<MeshDrawer>();


        vertexCalculator = playerBuildTool.GetComponent<CalculateVertexLocations>();
        vertexCalculator.SetCurrentConveyor(this);
        meshDrawer.template = template;

        SetFaces();
        Test();

        
    }

    private void Update()
    {
        //vertexCalculator.Calculate(Vector3.zero, playerBuildTool.GetRaycastHitPoint());
    }

    void Test()
    {
        vertexCalculator.Calculate(Vector3.zero, Vector3.forward);
    }

    void SetFaces()
    {
        if (template.frontFace != null)
        {
            Debug.Log("null");
        }
        frontFace = template.frontFace;
        backFace = template.backFace;

        frontFaceQuads = template.frontFaceQuads;
        backFaceQuads = template.backFaceQuads;
        otherQuads = template.otherQuads;
    }
}
