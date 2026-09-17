using UnityEngine;

public class LoopingRoad : MonoBehaviour
{
    public Transform road1, road2, road3; // Tiga segmen jalan
    private float roadLength; // Panjang satu segmen jalan
    public Transform player; // Target yang diikuti (motor)

    void Start()
    {
        if (road1 != null)
        {
            // Hitung panjang jalan berdasarkan Collider2D (misalnya Sprite Shape)
            roadLength = road1.GetComponent<Collider2D>().bounds.size.x;
        }
        else
        {
            Debug.LogError("Road1 belum di-assign!");
        }
    }

    void Update()
    {
        if (player == null) return;

        // Looping ke depan (motor ke kanan)
        if (player.position.x > road2.position.x + roadLength / 2)
        {
            MoveRoadForward();
        }

        // Looping ke belakang (motor ke kiri)
        if (player.position.x < road2.position.x - roadLength / 2)
        {
            MoveRoadBackward();
        }
    }

    void MoveRoadForward()
    {
        // Pindahkan road1 ke belakang setelah road3
        road1.position = new Vector3(road3.position.x + roadLength, road1.position.y, road1.position.z);

        // Geser urutan
        Transform temp = road1;
        road1 = road2;
        road2 = road3;
        road3 = temp;
    }

    void MoveRoadBackward()
    {
        // Pindahkan road3 ke depan sebelum road1
        road3.position = new Vector3(road1.position.x - roadLength, road3.position.y, road3.position.z);

        // Geser urutan
        Transform temp = road3;
        road3 = road2;
        road2 = road1;
        road1 = temp;
    }

    // Dipanggil dari GameManager untuk atur target pemain (motor)
    public void SetTarget(Transform newTarget)
    {
        player = newTarget;
    }
}
