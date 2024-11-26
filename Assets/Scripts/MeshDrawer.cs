using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class MeshDrawer : MonoBehaviour
{
    private Mesh mesh;
    private MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    private List<Vector3> vertices = new();
    private List<int> triangles = new();
    private List<Vector2> uvs = new();
    private int conveyorEndVerticesIndex;

    public MeshTemplate template;
    

    public bool draw;

    Vector2 uvBottomLeft = new Vector2(0, 0);
    Vector2 uvTopLeft = new Vector2(0, 1);
    Vector2 uvTopRight = new Vector2(1, 1);
    Vector2 uvBottomRight = new Vector2(1, 0);


    void Awake()
    {
        mesh = new Mesh();
        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        
    }

    public void SetMesh(Conveyor conveyor)
    {
        vertices.Clear();
        triangles.Clear();
        uvs.Clear();
        foreach (QuadVectors quadVectors in conveyor.frontFace)
        {
            SetQuad(quadVectors);
        }
        foreach (QuadVectors quadVectors in conveyor.backFace)
        {
            SetQuad(quadVectors);
        }
        foreach (QuadVectors quadVectors in conveyor.otherFaces)
        {
            SetQuad(quadVectors);
        }

        UpdateMesh();
    }

    

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();
        RecalculateFlatNormals(mesh);
    }

    void SetQuad(QuadVectors quad)
    {
        int index = vertices.Count;
        vertices.Add(quad.bottomLeft.position);
        vertices.Add(quad.topLeft.position);
        vertices.Add(quad.topRight.position);
        vertices.Add(quad.bottomRight.position);
        uvs.Add(uvBottomLeft);
        uvs.Add(uvBottomRight);
        uvs.Add(uvTopLeft);
        uvs.Add(uvTopRight);
        SetTriangle(index, 0, 1, 2);
        SetTriangle(index, 0, 2, 3);
    }

    void SetUVs()
    {

    }

    void SetTriangle(int index, int num1, int num2, int num3)
    {
        triangles.Add(num1 + index);
        triangles.Add(num2 + index);
        triangles.Add(num3 + index);
    }

    void DrawTest(float length)
    {
        //undo comments before using
        vertices.Clear();
        triangles.Clear();
        List<Vector3> newVertices = new();
        List<int> newTriangles = new();

        Vector3 vertex0 = new(0, 0, 0);
        Vector3 vertex1 = new Vector3(0, 1, 0);
        Vector3 vertex2 = new Vector3(1, 1, 0);
        Vector3 vertex3 = new Vector3(1, 0, 0);

        Vector3 vertex4 = new Vector3(0, 0, length);
        Vector3 vertex5 = new Vector3(0, 1, length);
        Vector3 vertex6 = new Vector3(1, 1, length);
        Vector3 vertex7 = new Vector3(1, 0, length);

        newVertices.Add(vertex0); // 0
        newVertices.Add(vertex1); // 1
        newVertices.Add(vertex2); // 2
        newVertices.Add(vertex3); // 3

        newVertices.Add(vertex4); // 4
        newVertices.Add(vertex5); // 5
        newVertices.Add(vertex1); // 6
        newVertices.Add(vertex0); // 7

        newVertices.Add(vertex7); // 8
        newVertices.Add(vertex6); // 9
        newVertices.Add(vertex5); // 10
        newVertices.Add(vertex4); // 11

        newVertices.Add(vertex3); // 12
        newVertices.Add(vertex2); // 13
        newVertices.Add(vertex6); // 14
        newVertices.Add(vertex7); // 15

        newVertices.Add(vertex1); // 16
        newVertices.Add(vertex5); // 17
        newVertices.Add(vertex6); // 18
        newVertices.Add(vertex2); // 19

        newTriangles.Add(0);
        newTriangles.Add(1);
        newTriangles.Add(2);

        newTriangles.Add(0);
        newTriangles.Add(2);
        newTriangles.Add(3);

        newTriangles.Add(4);
        newTriangles.Add(5);
        newTriangles.Add(6);

        newTriangles.Add(4);
        newTriangles.Add(6);
        newTriangles.Add(7);

        newTriangles.Add(8);
        newTriangles.Add(9);
        newTriangles.Add(10);

        newTriangles.Add(8);
        newTriangles.Add(10);
        newTriangles.Add(11);

        newTriangles.Add(12);
        newTriangles.Add(13);
        newTriangles.Add(14);

        newTriangles.Add(12);
        newTriangles.Add(14);
        newTriangles.Add(15);

        newTriangles.Add(16);
        newTriangles.Add(17);
        newTriangles.Add(18);

        newTriangles.Add(16);
        newTriangles.Add(18);
        newTriangles.Add(19);


        

        vertices.AddRange(newVertices);
        triangles.AddRange(newTriangles);

        Vector2 bottomLeft = new(0, 0);
        Vector2 topLeft = new(0, 1);
        Vector2 topRight = new(1, 1);
        Vector2 bottomRight = new(1, 0);

        //uvs = new Vector2[vertices.Count];

        uvs[0] = bottomLeft;
        uvs[1] = topLeft;
        uvs[2] = bottomRight;
        uvs[3] = topRight;

        uvs[4] = bottomRight;
        uvs[5] = topRight;
        uvs[6] = bottomLeft;
        uvs[7] = topLeft;

        uvs[8] = bottomRight;
        uvs[9] = topRight;
        uvs[10] = bottomLeft;
        uvs[11] = topLeft;

        uvs[12] = bottomRight;
        uvs[13] = topRight;
        uvs[14] = bottomLeft;
        uvs[15] = topLeft;

        uvs[16] = bottomRight;
        uvs[17] = topRight;
        uvs[18] = bottomLeft;
        uvs[19] = topLeft;


        UpdateMesh();

    }

    

    void RecalculateFlatNormals(Mesh mesh)
    {
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;
        Vector3[] normals = new Vector3[vertices.Length];

        for (int i = 0; i < triangles.Length; i += 3)
        {
            // Get the vertices of the triangle
            Vector3 v0 = vertices[triangles[i]];
            Vector3 v1 = vertices[triangles[i + 1]];
            Vector3 v2 = vertices[triangles[i + 2]];

            // Calculate the face normal
            Vector3 normal = Vector3.Cross(v1 - v0, v2 - v0).normalized;

            // Assign the same normal to all three vertices (flat shading)
            normals[triangles[i]] = normal;
            normals[triangles[i + 1]] = normal;
            normals[triangles[i + 2]] = normal;
        }

        // Apply the new normals to the mesh
        mesh.normals = normals;
    }

}
