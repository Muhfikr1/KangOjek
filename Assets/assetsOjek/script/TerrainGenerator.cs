using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int length = 100; // Panjang jalur yang di-generate
    public float scale = 5f; // Skala gelombang (ubah untuk tinggi/rendah bukit)
    public float speed = 1.5f; // Kecepatan looping jalan

    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;

    private List<Vector2> points = new List<Vector2>();

    void Start()
    {
        GenerateTerrain();
    }

    void GenerateTerrain()
    {
        points.Clear();
        Vector3[] positions = new Vector3[length];

        for (int i = 0; i < length; i++)
        {
            float x = i * 0.5f;
            float y = Mathf.PerlinNoise(i * 0.1f, 0) * scale; // Gunakan Perlin Noise untuk ketinggian

            positions[i] = new Vector3(x, y, 0);
            points.Add(new Vector2(x, y));
        }

        lineRenderer.positionCount = length;
        lineRenderer.SetPositions(positions);
        edgeCollider.SetPoints(points);
    }
}
