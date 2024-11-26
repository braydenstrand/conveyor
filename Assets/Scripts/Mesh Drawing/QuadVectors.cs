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

    public QuadVectors(Transform bottomLeft, Transform topLeft, Transform topRight, Transform bottomRight)
    {
        this.bottomLeft = bottomLeft;
        this.topLeft = topLeft;
        this.topRight = topRight;
        this.bottomRight = bottomRight;
    }



    public void Offset(Vector3 offset)
    {
        bottomLeft.position += offset;
        bottomRight.position += offset;
        topLeft.position += offset;
        topRight.position += offset;
    }
}
