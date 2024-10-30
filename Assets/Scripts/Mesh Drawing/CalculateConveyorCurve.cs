using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateConveyorCurve : MonoBehaviour
{
    [SerializeField] float radius;
    [SerializeField] float numberOfCubes;
    [SerializeField] GameObject prefab;
    [SerializeField] Vector3 bTest;
    [SerializeField] Vector3 cTest;
    [SerializeField] bool useTestPoints;
    public bool startCubePlaced;
    Vector3 startCubeLocation;
    Vector3 a;
    Vector3 b;
    Vector3 c;
    Vector3 d;
    Vector3 e;
    Vector3 o;
    float angleOBC;
    float angleOBCInRads;

    private void Start()
    {
        if (useTestPoints)
        {
            PlaceFirstCube(bTest);
            Calculate(cTest);
        }
    }

    private void Update()
    {
        if (useTestPoints)
        {
            PlaceFirstCube(bTest);
            Calculate(cTest);
        }
    }

    public void Calculate(Vector3 endCubeLocation)
    {
        Debug.Log("Calculating");

        CalculateCenter(endCubeLocation);

        CalculateTangents();

        GameObject[] points = new GameObject[(int)numberOfCubes];

        for (int i = 0; i < numberOfCubes; i++)
        {
            Debug.Log((1 / numberOfCubes) * i);
            GameObject point = PlaceCube(FindPointOnArc(o, d, e, (1 / numberOfCubes) * i));
            points[i] = point;
        }

        for (int i = 1; i < points.Length; i++) 
        {
            Debug.DrawLine(points[i].transform.position, points[i - 1].transform.position, Color.green);
            Debug.Log("test");
        }

        Debug.DrawLine(a, b, Color.red);
        Debug.DrawLine(c, b, Color.red);
    }

    Vector3 FindPointOnArc(Vector3 o, Vector3 d, Vector3 e, float t)
    {
        Vector3 dirA = (d - o).normalized;
        Vector3 dirC = (e - o).normalized;

        Vector3 pointOnArc = Vector3.Slerp(dirA, dirC, t);

        return o + pointOnArc * radius;
    }

    void CalculateTangents()
    {
        // Calculate angle BOE
        float angleBOE = 180 - (90 + angleOBC);
        Debug.Log("Angle BOE: " + angleBOE);

        // Calculate direction from O to B
        Vector3 ob = b - o;
        Debug.Log("OB: " +  ob);

        Quaternion rotationE = Quaternion.AngleAxis(-angleBOE, Vector3.up);
        Quaternion rotationD = Quaternion.AngleAxis(angleBOE, Vector3.up);

        Vector3 directionToE = rotationE * ob;
        Vector3 directionToD = rotationD * ob;

        e = o + directionToE.normalized * radius;
        d = o + directionToD.normalized * radius;
        Debug.Log("E: " + e);
        Debug.Log("D: " + d);
        //PlaceCube(e);
        //PlaceCube(d);
    }

    void CalculateCenter(Vector3 endCubeLocation)
    {
        // Set A B C
        a = startCubeLocation + new Vector3(0, 0, -10f);
        PlaceCube(a);
        b = startCubeLocation;
        c = endCubeLocation;
        PlaceCube(c);

        // Calculate angle ABC
        Vector3 ab = a - b;
        Debug.Log("Line AB: " + ab);
        Vector3 bc = c - b;
        Debug.Log("Line BC: " + bc);
        float angleABC = Vector3.Angle(ab, bc);
        Debug.Log("Angle ABC: " + angleABC);

        // Calculate angle OBC (o is center of circle)
        angleOBC = angleABC / 2;
        Debug.Log("Angle OBC: " + angleOBC);

        // Calculate length of line BO
        angleOBCInRads = angleOBC * Mathf.Deg2Rad;
        float lineOBLength = radius / Mathf.Sin(angleOBCInRads);
        Debug.Log("Line OB length: " + lineOBLength);

        // Calculate direction to center of circle
        Quaternion rotation = Quaternion.AngleAxis(-angleOBC, Vector3.up);
        Vector3 directionToCenter = rotation * bc;
        Debug.Log("Direction to Center: " + directionToCenter);

        // Calculate center of circle
        o = b + directionToCenter.normalized * lineOBLength;
        Debug.Log("Center of circle: " + o);
        PlaceCube(o);
    }

    public GameObject PlaceCube(Vector3 location)
    {
        GameObject cube = Instantiate(prefab, location, Quaternion.identity);
        return cube;
    }

    public void PlaceFirstCube(Vector3 location)
    {
        Instantiate(prefab, location, Quaternion.identity);
        startCubeLocation = location;
        startCubePlaced = true;
    }
}
