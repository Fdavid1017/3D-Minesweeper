using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    static int generatorCount = 0;

    public int xSize = 20;
    public int zSize = 20;
    [Range(0.0F, 10.0F)]
    public float yMultiply = 2f;

    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    GameObject cubeParent;

    private void Awake()
    {
        generatorCount++;
        cubeParent = new GameObject();
        cubeParent.name = generatorCount.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        UnityEditor.SceneView.FocusWindowIfItsOpen(typeof(UnityEditor.SceneView));

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
    }

    private void Update()
    {

    }

    private void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        int randomX = UnityEngine.Random.Range(0, 100);
        int randomZ = UnityEngine.Random.Range(0, 100);
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                int y = Mathf.RoundToInt(Mathf.PerlinNoise(randomX * .3f, randomZ * .3f) * yMultiply);
                vertices[i] = new Vector3(x, y, z);
                i++;
                randomX++;
                randomZ++;
            }
        }

        int vert = 0;
        int tris = 0;
        triangles = new int[xSize * zSize * 6];

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }
    }

    private void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    private void OnDrawGizmos()
    {
        if (vertices == null)
        {
            return;
        }
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], .1f);
        }
    }
}
