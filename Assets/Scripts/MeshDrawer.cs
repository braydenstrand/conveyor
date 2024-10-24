using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class MeshDrawer : MonoBehaviour
{
    private Mesh mesh;
    private MeshFilter meshFilter;
    private List<Vector3> vertices = new();
    private List<int> triangles = new();
    private int conveyorEndVerticesIndex;

    [SerializeField] private Vector3 startingPos;
    [SerializeField] private float initialLength;

    [Header("Conveyor Dimensions")]
    //[SerializeField] private Vector2 bottomLeft;
    //[SerializeField] private Vector2 lPostTopLeft;
    //[SerializeField] private Vector2 lPostTopRight;
    //[SerializeField] private Vector2 lPostBottomRight;
    //[SerializeField] private Vector2 rPostBottomLeft;
    //[SerializeField] private Vector2 rPostTopLeft;
    //[SerializeField] private Vector2 rPostTopRight;
    //[SerializeField] private Vector2 bottomRight;
    [SerializeField] private float width;
    [SerializeField] private float height;
    [SerializeField] private float postWidth;
    [SerializeField] private float postHeight;

    [Header("Pole Dimensions")]
    [SerializeField] private float poleWidth;
    [SerializeField] private float poleHeight;
    [SerializeField] private float poleDepth;


    void Start()
    {
        mesh = new Mesh();
        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;
        //InitializeBuidTool();
        DrawTest();
    }

    void Update()
    {
        
    }

    public void InitializeBuidTool()
    {
        Debug.Log("Initializing Build Tool");
        DrawEndOfConveyor(new Vector3(startingPos.x, startingPos.y + poleHeight, startingPos.z - (initialLength / 2)), transform.forward);
        DrawEndOfConveyor(new Vector3(startingPos.x, startingPos.y + poleHeight, startingPos.z + (initialLength / 2)), -transform.forward);
        DrawInBetweenEnds();
        DrawConveyorPole(startingPos);

        SetUVCoordinates();
    }

    void UpdateMesh()
    {
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        RecalculateFlatNormals(mesh);

    }

    void DrawTest()
    {
        List<Vector3> newVertices = new();
        List<int> newTriangles = new();
        List<Vector2> newUvs = new();

        Vector3 edgeBottom = new(0, 0, 0);
        Vector3 edgeTop = new(0, 1, 0);
        newVertices.Add(edgeBottom); // 0
        newVertices.Add(edgeTop); // 1
        newVertices.Add(edgeBottom); // 2
        newVertices.Add(edgeTop); // 3
        newVertices.Add(new Vector3(1, 0, 0)); // 4
        newVertices.Add(new Vector3(1, 1, 0)); // 5
        newVertices.Add(new Vector3(0, 0, 1)); // 6
        newVertices.Add(new Vector3(0, 1, 1)); // 7

        newTriangles.Add(0);
        newTriangles.Add(1);
        newTriangles.Add(5);

        newTriangles.Add(0);
        newTriangles.Add(5);
        newTriangles.Add(4);

        newTriangles.Add(6);
        newTriangles.Add(7);
        newTriangles.Add(3);

        newTriangles.Add(6);
        newTriangles.Add(3);
        newTriangles.Add(2);

        vertices.AddRange(newVertices);
        triangles.AddRange(newTriangles);

        UpdateMesh();

    }

    void SetUVCoordinates()
    {
        Vector2[] UVs = new Vector2[mesh.vertices.Length];

        UVs[0] = new Vector2(0, 0);
        UVs[1] = new Vector2(0, 1);
        UVs[6] = new Vector2(1, 1);
        UVs[7] = new Vector2(1, 0);

        mesh.uv = UVs;

        Debug.Log(mesh.uv[0]);
        Debug.Log(mesh.uv[1]);
        Debug.Log(mesh.uv[6]);
        Debug.Log(mesh.uv[7]);
        Debug.Log(mesh.vertices[0]);
        Debug.Log(mesh.vertices[1]);
        Debug.Log(mesh.vertices[6]);
        Debug.Log(mesh.vertices[7]);
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


    void DrawEndOfConveyor(Vector3 startingPos, Vector3 direction)
    {
        List<Vector3> newVertices = new();
        List<int> newTriangles = new();
        int currentVerticesLength = vertices.Count;
        conveyorEndVerticesIndex = vertices.Count;


        Vector3 right = Vector3.Cross(Vector3.up, direction).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);

        newVertices.Add(startingPos + new Vector3(-width / 2, 0, 0)); // 0
        newVertices.Add(startingPos + new Vector3(-width / 2, height, 0)); // 1
        newVertices.Add(startingPos + new Vector3((-width / 2) + postWidth, height, 0)); // 2
        newVertices.Add(startingPos + new Vector3((-width / 2) + postWidth, height - postHeight, 0)); // 3
        newVertices.Add(startingPos + new Vector3((width / 2) - postWidth, height - postHeight, 0)); // 4
        newVertices.Add(startingPos + new Vector3((width / 2) - postWidth, height, 0)); // 5
        newVertices.Add(startingPos + new Vector3(width / 2, height, 0)); // 6
        newVertices.Add(startingPos + new Vector3(width / 2, 0, 0)); // 7
        newVertices.Add(startingPos + new Vector3((width / 2) - postWidth, 0, 0)); // 8
        newVertices.Add(startingPos + new Vector3((-width / 2) + postWidth, 0, 0)); // 9

        for (int i = 0; i < newVertices.Count; i++)
        {
            newVertices[i] = newVertices[i] - startingPos;

            newVertices[i] = rotation * newVertices[i] + startingPos;
        }

        newTriangles.Add(currentVerticesLength + 0);
        newTriangles.Add(currentVerticesLength + 1);
        newTriangles.Add(currentVerticesLength + 9);

        newTriangles.Add(currentVerticesLength + 1);
        newTriangles.Add(currentVerticesLength + 2);
        newTriangles.Add(currentVerticesLength + 9);

        newTriangles.Add(currentVerticesLength + 9);
        newTriangles.Add(currentVerticesLength + 3);
        newTriangles.Add(currentVerticesLength + 8);

        newTriangles.Add(currentVerticesLength + 3);
        newTriangles.Add(currentVerticesLength + 4);
        newTriangles.Add(currentVerticesLength + 8);

        newTriangles.Add(currentVerticesLength + 8);
        newTriangles.Add(currentVerticesLength + 5);
        newTriangles.Add(currentVerticesLength + 6);

        newTriangles.Add(currentVerticesLength + 8);
        newTriangles.Add(currentVerticesLength + 6);
        newTriangles.Add(currentVerticesLength + 7);


        vertices.AddRange(newVertices);
        triangles.AddRange(newTriangles);


        UpdateMesh();

        
    }

    void DrawInBetweenEnds()
    {
        List<int> newTriangles = new()
        {
            conveyorEndVerticesIndex + 7,
            conveyorEndVerticesIndex + 6,
            0,

            conveyorEndVerticesIndex + 6,
            1,
            0,

            conveyorEndVerticesIndex + 6,
            conveyorEndVerticesIndex + 5,
            1,

            conveyorEndVerticesIndex + 5,
            2,
            1,

            3,
            2,
            conveyorEndVerticesIndex + 5,

            3,
            conveyorEndVerticesIndex + 5,
            conveyorEndVerticesIndex + 4,

            conveyorEndVerticesIndex + 3,
            conveyorEndVerticesIndex + 2,
            4,

            conveyorEndVerticesIndex + 4,
            conveyorEndVerticesIndex + 3,
            4,

            conveyorEndVerticesIndex + 4,
            4,
            3,

            conveyorEndVerticesIndex + 2,
            5,
            4,

            conveyorEndVerticesIndex + 2,
            conveyorEndVerticesIndex + 1,
            5,

            conveyorEndVerticesIndex + 1,
            6,
            5,

            7,
            6,
            conveyorEndVerticesIndex + 0,

            6,
            conveyorEndVerticesIndex + 1,
            conveyorEndVerticesIndex + 0,

            conveyorEndVerticesIndex + 0,
            conveyorEndVerticesIndex + 7,
            7,

            conveyorEndVerticesIndex + 7,
            0,
            7

        };

        triangles.AddRange(newTriangles);

        UpdateMesh();
    }

    void DrawConveyorPole(Vector3 startingPos)
    {
        startingPos = (new Vector3(startingPos.x - (poleWidth / 2), startingPos.y, startingPos.z));
        List<Vector3> newVertices = new();
        List<int> newTriangles = new();
        int currentVerticesLength = vertices.Count;

        newVertices.Add(startingPos); // 0
        newVertices.Add(startingPos + new Vector3(0, poleHeight, 0)); // 1
        newVertices.Add(startingPos + new Vector3(poleWidth, poleHeight, 0)); // 2
        newVertices.Add(startingPos + new Vector3(poleWidth, 0, 0)); // 3
        newVertices.Add(startingPos + new Vector3(0, 0, poleDepth)); // 4
        newVertices.Add(startingPos + new Vector3(0, poleHeight, poleDepth)); // 5
        newVertices.Add(startingPos + new Vector3(poleWidth, poleHeight, poleDepth)); // 6
        newVertices.Add(startingPos + new Vector3(poleWidth, 0, poleDepth)); // 7

        newTriangles.Add(currentVerticesLength + 0);
        newTriangles.Add(currentVerticesLength + 1);
        newTriangles.Add(currentVerticesLength + 2);

        newTriangles.Add(currentVerticesLength + 0);
        newTriangles.Add(currentVerticesLength + 2);
        newTriangles.Add(currentVerticesLength + 3);

        newTriangles.Add(currentVerticesLength + 4);
        newTriangles.Add(currentVerticesLength + 5);
        newTriangles.Add(currentVerticesLength + 1);

        newTriangles.Add(currentVerticesLength + 4);
        newTriangles.Add(currentVerticesLength + 1);
        newTriangles.Add(currentVerticesLength + 0);

        newTriangles.Add(currentVerticesLength + 7);
        newTriangles.Add(currentVerticesLength + 6);
        newTriangles.Add(currentVerticesLength + 5);

        newTriangles.Add(currentVerticesLength + 7);
        newTriangles.Add(currentVerticesLength + 5);
        newTriangles.Add(currentVerticesLength + 4);

        newTriangles.Add(currentVerticesLength + 3);
        newTriangles.Add(currentVerticesLength + 2);
        newTriangles.Add(currentVerticesLength + 6);

        newTriangles.Add(currentVerticesLength + 3);
        newTriangles.Add(currentVerticesLength + 6);
        newTriangles.Add(currentVerticesLength + 7);

        vertices.AddRange(newVertices);
        triangles.AddRange(newTriangles);


        UpdateMesh();
    }




    void GenerateRectangularPrismMesh(List<Vector3> curvePoints, float width, float height)
    {

        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        Vector3 lastUp = Vector3.up; // Helps orient the prism in 3D space.

        for (int i = 0; i < curvePoints.Count - 1; i++)
        {
            Vector3 p0 = curvePoints[i];
            Vector3 p1 = curvePoints[i + 1];

            // Calculate the direction between the current and next point
            Vector3 direction = (p1 - p0).normalized;

            // Calculate the right and up vectors to orient the rectangle
            Vector3 right = Vector3.Cross(Vector3.up, direction).normalized * width;
            Vector3 up = Vector3.Cross(direction, right).normalized * height;

            if (i == 0)
            {
                lastUp = up;  // Keep consistent orientation along the curve
            }

            // Define the four corner points of the rectangle at p0
            Vector3 topLeft = p0 - right * 0.5f + lastUp * 0.5f;
            Vector3 topRight = p0 + right * 0.5f + lastUp * 0.5f;
            Vector3 bottomLeft = p0 - right * 0.5f - lastUp * 0.5f;
            Vector3 bottomRight = p0 + right * 0.5f - lastUp * 0.5f;

            // Define the four corner points of the rectangle at p1
            Vector3 nextTopLeft = p1 - right * 0.5f + up * 0.5f;
            Vector3 nextTopRight = p1 + right * 0.5f + up * 0.5f;
            Vector3 nextBottomLeft = p1 - right * 0.5f - up * 0.5f;
            Vector3 nextBottomRight = p1 + right * 0.5f - up * 0.5f;

            // Add vertices for the current segment (2 sets of 4 vertices)
            vertices.Add(topLeft);      // 0
            vertices.Add(topRight);     // 1
            vertices.Add(bottomLeft);   // 2
            vertices.Add(bottomRight);  // 3

            vertices.Add(nextTopLeft);  // 4
            vertices.Add(nextTopRight); // 5
            vertices.Add(nextBottomLeft); // 6
            vertices.Add(nextBottomRight); // 7

            int baseIndex = i * 8;

            // Add triangles for the top quad
            triangles.Add(baseIndex + 0);
            triangles.Add(baseIndex + 4);
            triangles.Add(baseIndex + 5);

            triangles.Add(baseIndex + 0);
            triangles.Add(baseIndex + 5);
            triangles.Add(baseIndex + 1);

            // Add triangles for the bottom quad
            triangles.Add(baseIndex + 2);
            triangles.Add(baseIndex + 6);
            triangles.Add(baseIndex + 7);

            triangles.Add(baseIndex + 2);
            triangles.Add(baseIndex + 7);
            triangles.Add(baseIndex + 3);

            // Add triangles for the sides (connecting the quads)
            triangles.Add(baseIndex + 0);
            triangles.Add(baseIndex + 2);
            triangles.Add(baseIndex + 6);

            triangles.Add(baseIndex + 0);
            triangles.Add(baseIndex + 6);
            triangles.Add(baseIndex + 4);

            triangles.Add(baseIndex + 1);
            triangles.Add(baseIndex + 3);
            triangles.Add(baseIndex + 7);

            triangles.Add(baseIndex + 1);
            triangles.Add(baseIndex + 7);
            triangles.Add(baseIndex + 5);

            // Update the last up vector for smooth orientation
            lastUp = up;
        }

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();

        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;
    }

}
