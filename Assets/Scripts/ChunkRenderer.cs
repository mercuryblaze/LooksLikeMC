using System;
using System.Collections.Generic;
using UnityEditor.Overlays;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ChunkRenderer : MonoBehaviour
{
    private const int _chunkWidth = 10;
    private const int _chunkHeight = 128;
    private List<Vector3> _verticies = new List<Vector3>();
    private List<int> _triangles = new List<int>();

    public int[,,] Blocks = new int[_chunkWidth, _chunkHeight, _chunkWidth];

    private void Start()
    {
        Mesh _chunkMesh = new Mesh();

        Blocks[0, 0, 0] = 1;

        for (int y = 0; y < _chunkHeight; y++)
        {
            for (int x = 0; x < _chunkWidth; x++)
            {
                for (int z = 0; z < _chunkWidth; z++)
                {
                    GenerateBlock(x, y, z);
                }
            }
        }

        _chunkMesh.vertices = _verticies.ToArray();
        _chunkMesh.triangles = _triangles.ToArray();

        _chunkMesh.RecalculateNormals();
        _chunkMesh.RecalculateBounds();

        GetComponent<MeshFilter>().mesh = _chunkMesh;
    }

    private void GenerateBlock(int x, int y, int z)
    {
        if (Blocks[x, y, z] == 0) return;

        Vector3Int blockPosition = new Vector3Int(x, y, z);

        GenerateRightSide(blockPosition);
        GenerateLeftSide(blockPosition);
    }

    private void GenerateRightSide(Vector3Int blockPosition)
    {
        _verticies.Add(new Vector3(0, 0, 0) + blockPosition);
        _verticies.Add(new Vector3(0, 1, 0) + blockPosition);
        _verticies.Add(new Vector3(0, 0, 1) + blockPosition);
        _verticies.Add(new Vector3(0, 1, 1) + blockPosition);

        AddLastVerticiesSquare();
    }

    private void GenerateLeftSide(Vector3Int blockPosition)
    {
        _verticies.Add(new Vector3(1, 0, 0) + blockPosition);
        _verticies.Add(new Vector3(1, 0, 1) + blockPosition);
        _verticies.Add(new Vector3(1, 1, 0) + blockPosition);
        _verticies.Add(new Vector3(1, 1, 1) + blockPosition);

        AddLastVerticiesSquare();
    }

    private void AddLastVerticiesSquare()
    {
        _triangles.Add(_verticies.Count - 4);
        _triangles.Add(_verticies.Count - 3);
        _triangles.Add(_verticies.Count - 2);

        _triangles.Add(_verticies.Count - 3);
        _triangles.Add(_verticies.Count - 1);
        _triangles.Add(_verticies.Count - 2);
    }
}