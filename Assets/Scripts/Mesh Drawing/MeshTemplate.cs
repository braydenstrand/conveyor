using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTemplate : MonoBehaviour
{
    [Header("Faces")]
    [SerializeField] GameObject frontFace;
    [SerializeField] GameObject backFace;

    [Header("Quads")]
    [SerializeField] List<QuadVectors> frontFaceQuads = new();
    [SerializeField] List<QuadVectors> backFaceQuads = new();
    [SerializeField] List<QuadVectors> otherQuads = new();

    private void Start()
    {
        DrawTest();
    }

    public void DrawTest()
    {

    }
}
