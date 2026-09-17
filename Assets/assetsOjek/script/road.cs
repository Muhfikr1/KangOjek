using UnityEngine;
using UnityEngine.U2D;

public class InfiniteRoad : MonoBehaviour
{
    public float speed = 2f; // Kecepatan pergerakan jalan
    public SpriteShapeController shapeController; // Sprite Shape Controller

    private Spline spline;
    private float moveDistance = 2f; // Jarak perpindahan sebelum menambahkan titik baru
    private float lastXPosition;

    void Start()
    {
        spline = shapeController.spline;
        lastXPosition = spline.GetPosition(spline.GetPointCount() - 1).x; // Posisi titik terakhir spline
    }

    void Update()
    {
        // Geser semua titik ke kiri untuk menciptakan efek berjalan
        for (int i = 0; i < spline.GetPointCount(); i++)
        {
            Vector3 pos = spline.GetPosition(i);
            pos.x -= speed * Time.deltaTime;
            spline.SetPosition(i, pos);
        }

        // Jika titik terakhir melewati batas tertentu, tambahkan titik baru di depan
        if (spline.GetPosition(spline.GetPointCount() - 1).x < lastXPosition - moveDistance)
        {
            AddNewPoint();
        }
    }

    void AddNewPoint()
    {
        Vector3 lastPos = spline.GetPosition(spline.GetPointCount() - 1);
        Vector3 newPos = new Vector3(lastPos.x + moveDistance, lastPos.y, lastPos.z);

        spline.InsertPointAt(spline.GetPointCount(), newPos); // Tambahkan titik baru
        spline.SetTangentMode(spline.GetPointCount() - 1, ShapeTangentMode.Continuous); // Supaya smooth

        lastXPosition = newPos.x;
    }
}
