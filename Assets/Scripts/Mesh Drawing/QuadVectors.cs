using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuadVectors
{
    public Transform bottomLeft;
    public Transform topLeft;
    public Transform topRight;
    public Transform bottomRight;

    public Vector3 bottomLeftOrg;
    public Vector3 topLeftOrg;
    public Vector3 bottomRightOrg;
    public Vector3 topRightOrg;

    public QuadVectors(Transform bottomLeft, Transform topLeft, Transform topRight, Transform bottomRight)
    {
        this.bottomLeft = bottomLeft;
        this.topLeft = topLeft;
        this.topRight = topRight;
        this.bottomRight = bottomRight;
    }

    public void Initialize()
    {
        bottomLeftOrg = bottomLeft.localPosition;
        topLeftOrg = topLeft.localPosition;
        bottomRightOrg = bottomRight.localPosition;
        topRightOrg = topRight.localPosition;
    }

    public void OffsetEachDraw()
    {
        //bottomLeft.position -= bottomLeft.position - bottomLeftOrg;
        //bottomRight.position -= bottomRight.position - bottomRightOrg;
        //topLeft.position -= topLeft.position - topLeftOrg;
        //topRight.position -= topRight.position - topRightOrg;
        

        bottomLeft.localPosition -= bottomLeft.localPosition;
        bottomRight.localPosition -= bottomRight.localPosition;
        topLeft.localPosition -= topLeft.localPosition;
        topRight.localPosition -= topRight.localPosition;

        
    }

    public void Offset(Vector3 offset)
    {
        bottomLeft.position += offset;
        bottomRight.position += offset;
        topLeft.position += offset;
        topRight.position += offset;
    }
}
