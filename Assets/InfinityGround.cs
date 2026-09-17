using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class InfiniteGround : MonoBehaviour
{
    public SpriteShapeController spriteShape;
    public Transform player; // Player yang bergerak
    public float segmentWidth = 10f; // Lebar tiap segmen
    public float yMultiplier = 3f; // Ketinggian perlin noise
    public float noiseStep = 0.5f; // Perbedaan noise antar segmen
    public int maxSegments = 5; // Jumlah segmen aktif dalam satu waktu

    private List<Vector3> points = new List<Vector3>(); // List posisi titik ground
    private float lastXPos = 0f;
    private int segmentCount = 0;

    void Start()
    {
        GenerateInitialSegments();
    }

    void Update()
    {
        // Jika player sudah dekat ke ujung segmen terakhir, buat segmen baru
        if (player.position.x > lastXPos - (segmentWidth * 2))
        {
            GenerateNextSegment();
        }
    }

    void GenerateInitialSegments()
    {
        for (int i = 0; i < maxSegments; i++)
        {
            GenerateNextSegment();
        }
    }

    void GenerateNextSegment()
    {
        float xPos = lastXPos + segmentWidth;
        float yPos = Mathf.PerlinNoise(xPos * noiseStep, 0) * yMultiplier;

        Vector3 newPoint = new Vector3(xPos, yPos);
        points.Add(newPoint);

        // Tambahkan titik baru ke Spline
        spriteShape.spline.InsertPointAt(segmentCount, newPoint);
        spriteShape.spline.SetTangentMode(segmentCount, ShapeTangentMode.Continuous);

        // Update posisi terakhir
        lastXPos = xPos;
        segmentCount++;

        // Hapus titik lama jika terlalu banyak
        if (points.Count > maxSegments)
        {
            points.RemoveAt(0);
            spriteShape.spline.RemovePointAt(0);
        }
    }
}
