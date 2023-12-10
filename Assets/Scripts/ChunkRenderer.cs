using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ChunkRenderer : MonoBehaviour
{
    private const int _chunkWidth = 10;
    private const int _chunkHeight = 128;
    private List<Vector3> _verticies = new List<Vector3>();
    private List<int> _triangles = new List<int>();

    public int[,,] Blocks = new int[_chunkWidth, _chunkWidth, _chunkHeight];

    private void Start()
    {
        Mesh _chunkMesh = new Mesh();

        _verticies.Add(new Vector3(0, 0, 0));
        _verticies.Add(new Vector3(0, 1, 0));
        _verticies.Add(new Vector3(0, 0, 1));
        _verticies.Add(new Vector3(0, 1, 1));

        _triangles.Add(0);
        _triangles.Add(1);
        _triangles.Add(2);

        _triangles.Add(1);
        _triangles.Add(3);
        _triangles.Add(2);

        _chunkMesh.vertices = _verticies.ToArray();
        _chunkMesh.triangles = _triangles.ToArray();

        _chunkMesh.RecalculateNormals();
        _chunkMesh.RecalculateBounds();

        GetComponent<MeshFilter>().mesh = _chunkMesh;
    }
}