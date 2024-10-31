using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTemplate : MonoBehaviour
{
    [Header("Faces")]
    public GameObject frontFace;
    public GameObject backFace;

    [Header("Quads")]
    public List<QuadVectors> frontFaceQuads = new();
    public List<QuadVectors> backFaceQuads = new();
    public List<QuadVectors> otherQuads = new();

    
}
