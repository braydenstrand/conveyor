using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base Conveyor Class
/// </summary>

public class Conveyor : MonoBehaviour
{

    [HideInInspector] public List<QuadVectors> frontFace = new();
    [HideInInspector] public List<QuadVectors> backFace = new();
    [HideInInspector] public List<QuadVectors> otherFaces = new();
    [HideInInspector] public Transform frontFacePoint;

    [HideInInspector] public List<QuadVectors> quads = new();
    [HideInInspector] public List<Transform> frontFaceVertices = new();
    [HideInInspector] public List<Vector3> backFaceVertices = new();
    [HideInInspector] public List<Vector3> otherVertices = new();

    Mesh beltMesh;
    Mesh frameMesh;

    CalculateVertexLocations vertexCalculator;
    MeshDrawer meshDrawer;
    MeshTemplate template;
    PlayerBuildTool playerBuildTool;

    public GameObject conveyorTemplateObject;
    [SerializeField] Material validBuildMaterial;
    [SerializeField] Material invalidBuildMaterial;
    [SerializeField] Material builtMaterial;
    public bool draw;

    private void Awake()
    {
        beltMesh = new Mesh();
        frameMesh = new Mesh();

        playerBuildTool = FindObjectOfType<PlayerBuildTool>();
        template = conveyorTemplateObject.GetComponent<MeshTemplate>();
        InitializeFaces();
        meshDrawer = GetComponent<MeshDrawer>();


        vertexCalculator = GetComponent<CalculateVertexLocations>();
        vertexCalculator.SetCurrentConveyor(this);
        
        meshDrawer.template = template;

        vertexCalculator.Initialize(playerBuildTool, this);
        

        
    }

    /// <summary>
    /// Set Conveyor Material (1 = valid build, 2 = invalid build, 3 = built)
    /// </summary>
    public void SetMaterial(int num)
    {
        if (meshDrawer.meshRenderer == null)
        {
            Debug.Log("Test");
        }
        if (meshDrawer == null)
        {
            Debug.Log("Test");
        }
        switch (num)
        {
            case 1:
                meshDrawer.meshRenderer.material = validBuildMaterial;
                break;
            case 2:
                meshDrawer.meshRenderer.material = invalidBuildMaterial;
                break;
            case 3:
                meshDrawer.meshRenderer.material = builtMaterial;
                break;
        }
    }

    public void Calculate()
    {
        vertexCalculator.Calculate();
    }

    private void Update()
    {
        if (draw)
        {
            meshDrawer.SetMesh(this);
        }
        
    }

    void Test()
    {
        vertexCalculator.Calculate();
        meshDrawer.SetMesh(this);
    }

    void InitializeFaces()
    {
        frontFace = template.frontFaceQuads;
        backFace = template.backFaceQuads;
        otherFaces = template.otherQuads;
        frontFacePoint = template.frontFacePoint;

        
        List<Transform> faces = new();
        List<Transform> quads = new();
        List<Transform> vertices = new();

        Transform parentObject = template.gameObject.transform;
        int childCount = parentObject.transform.childCount;

        Transform currentChild;

        for (int i = 0; i < childCount; i++)
        {
            currentChild = parentObject.GetChild(i);
            if (currentChild.CompareTag("FrontFace"))
            {
                faces.Add(currentChild);
            }
        }

        foreach (Transform face in faces)
        {
            childCount = face.childCount;
            for(int i = 0; i < childCount; i++)
            {
                currentChild = face.GetChild(i);
                if (currentChild.CompareTag("Quad"))
                {
                    quads.Add(currentChild);
                }
            }
        }

        foreach(Transform quad in quads)
        {
            childCount = quad.childCount;
            for (int i = 0; i < childCount; i++)
            {
                currentChild = quad.GetChild(i);
                if (currentChild.CompareTag("FrontVertex"))
                {
                    vertices.Add(currentChild);
                }
            }
        }
        frontFaceVertices.AddRange(vertices);

        //Debug.Log(frontFaceVertices[0]);
        //Debug.Log(frontFace[0].bottomLeft.position);
        //frontFaceVertices[0] += new Vector3(1, 1, 1);
        //frontFace[0].bottomLeft.position += new Vector3(1, 1, 1);
        //Debug.Log(frontFaceVertices[0]);
        //Debug.Log(frontFace[0].bottomLeft.position);
    }
}
