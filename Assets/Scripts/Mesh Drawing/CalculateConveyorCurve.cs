using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateConveyorCurve : MonoBehaviour
{
    [SerializeField] float width;
    [SerializeField] float radius;
    [SerializeField] float numberOfPoints;
    GameObject[] points;
    GameObject[] side1Points;
    GameObject[] side2Points;
    GameObject startingPoint;
    [SerializeField] GameObject prefab;
    [SerializeField] Transform aTest;
    [SerializeField] Transform bTest;
    [SerializeField] Transform cTest;
    [SerializeField] Transform bTestTwo;
    [SerializeField] Transform cTestTwo;
    [SerializeField] bool useTestPoints;
    public bool startPointPlaced;
    Vector3 startPointLocation;
    Vector3 a;
    Vector3 b;
    Vector3 c;
    Vector3 d;
    Vector3 e;
    Vector3 o;
    GameObject oObject;
    GameObject aObject;
    GameObject cObject;
    float angleOBC;
    float angleOBCInRads;
    float totalRotation;

    private void Start()
    {
        Initialize();
        if (useTestPoints)
        {
            PlaceFirstPoint(bTest.transform.position);
            Calculate(cTest.transform.position);
            //PlaceFirstPoint(bTestTwo.transform.position);
            //Calculate(cTestTwo.transform.position);
        }
        else
        {
            PlaceFirstPoint(bTest.transform.position);
        }
    }

    private void Update()
    {
        if (useTestPoints)
        {
            PlaceFirstPoint(bTest.transform.position);
            Calculate(cTest.transform.position);
            //PlaceFirstPoint(bTestTwo.transform.position);
            //Calculate(cTestTwo.transform.position);
        }
        else
        {
            PlaceFirstPoint(bTest.transform.position);
        }
    }

    public void Initialize()
    {
        side1Points = new GameObject[(int)numberOfPoints];
        side2Points = new GameObject[(int)numberOfPoints];
        points = new GameObject[(int)numberOfPoints];
        startingPoint = Instantiate(prefab, aTest.position, aTest.rotation);
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = Instantiate(prefab, aTest.position, aTest.rotation);
        }
        for (int i = 0; i < side1Points.Length; i++)
        {
            side1Points[i] = Instantiate(prefab, aTest.position, aTest.rotation);
        }

        for (int i = 0; i < side2Points.Length; i++)
        {
            side2Points[i] = Instantiate(prefab, aTest.position, aTest.rotation);
        }
        oObject = Instantiate(prefab);
        aObject = Instantiate(prefab);
        cObject = Instantiate(prefab);
    }

    public void Calculate(Vector3 endPointLocation)
    {
        Debug.Log("Calculating");

        CalculateCenter(endPointLocation);

        CalculateTangents();

        

        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i].transform.rotation = aTest.transform.rotation;
            Debug.Log((1 / numberOfPoints) * i);
            points[i].transform.position = FindPointOnArc(o, d, e, (1 / numberOfPoints) * i);
            points[i].transform.Rotate(new Vector3(0, (totalRotation / (numberOfPoints - 1)) * i, 0));
            //side1Points[i].transform.SetLocalPositionAndRotation(new Vector3(points[i].transform.position.x - width / 2, points[i].transform.position.y, points[i].transform.position.z), points[i].transform.rotation);
            //side2Points[i].transform.SetLocalPositionAndRotation(new Vector3(points[i].transform.position.x + width / 2, points[i].transform.position.y, points[i].transform.position.z), points[i].transform.rotation);


            side1Points[i].transform.SetLocalPositionAndRotation(points[i].transform.position, points[i].transform.rotation);
            side2Points[i].transform.SetLocalPositionAndRotation(points[i].transform.position, points[i].transform.rotation);

            side1Points[i].transform.Translate(Vector3.left * (width / 2));
            side2Points[i].transform.Translate(Vector3.right * (width / 2));
        }

        for (int i = 1; i < points.Length; i++) 
        {
            Debug.DrawLine(points[i].transform.position, points[i - 1].transform.position, Color.green);
            Debug.DrawLine(side1Points[i].transform.position, side1Points[i - 1].transform.position, Color.green);
            Debug.DrawLine(side2Points[i].transform.position, side2Points[i - 1].transform.position, Color.green);
            
            Debug.Log("test");
        }

        Debug.DrawLine(points[(int)numberOfPoints - 1].transform.position, side1Points[(int)numberOfPoints - 1].transform.position);
        Debug.DrawLine(points[(int)numberOfPoints - 1].transform.position, side2Points[(int)numberOfPoints - 1].transform.position);

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

    void CalculateCenter(Vector3 endPointLocation)
    {
        // Set A B C
        a = startPointLocation + new Vector3(0, 0, -10f);
        aObject.transform.position = a;
        b = startPointLocation;
        c = endPointLocation;
        cObject.transform.position = c;

        // Calculate angle ABC
        Vector3 ab = a - b;
        Debug.Log("Line AB: " + ab);
        Vector3 bc = c - b;
        Debug.Log("Line BC: " + bc);
        float angleABC = Vector3.Angle(ab, bc);
        Debug.Log("Angle ABC: " + angleABC);
        totalRotation = angleABC - 180;

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
        oObject.transform.position = o;
    }

    public GameObject PlacePoint(Vector3 location)
    {
        GameObject cube = Instantiate(prefab, location, Quaternion.identity);
        return cube;
    }

    public void PlaceFirstPoint(Vector3 location)
    {
        startingPoint.transform.position = location;
        startPointLocation = location;
        startPointPlaced = true;
    }
}
