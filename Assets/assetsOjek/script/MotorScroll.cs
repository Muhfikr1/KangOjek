using UnityEngine;

[ExecuteAlways]
public class Motormanage : MonoBehaviour
{
    public GameObject[] motors; // Array menyimpan semua motor
    [SerializeField] private Vector2 previewViewportPosition = new Vector2(0.52f, 0.36f);
    private int currentIndex = 0; // Indeks motor saat ini

    private void OnEnable()
    {
        SyncCurrentIndex();
        PrepareMotorsForMenu();
        PlaceCurrentMotorInCamera();
    }

    private void Start()
    {
        SetActiveMotor(currentIndex);
        PrepareMotorsForMenu();
        PlaceCurrentMotorInCamera();
    }

    private void LateUpdate()
    {
        if (!Application.isPlaying)
        {
            SyncCurrentIndex();
        }

        PrepareMotorsForMenu();
        PlaceCurrentMotorInCamera();
    }

    public void NextMotor()
    {
        ChangeMotor(1); // Pindah ke motor berikutnya
    }

    public void PreviousMotor()
    {
        ChangeMotor(-1); // Pindah ke motor sebelumnya
    }

    private void ChangeMotor(int direction)
    {
        if (motors == null || motors.Length == 0) return;

        // Pindah ke motor baru (looping)
        currentIndex = (currentIndex + direction + motors.Length) % motors.Length;
        SetActiveMotor(currentIndex);
        PlaceCurrentMotorInCamera();
    }

    private void SetActiveMotor(int index)
    {
        for (int i = 0; i < motors.Length; i++)
        {
            if (motors[i] != null)
            {
                motors[i].SetActive(i == index);
            }
        }
    }

    private void SyncCurrentIndex()
    {
        if (motors == null) return;

        for (int i = 0; i < motors.Length; i++)
        {
            if (motors[i] != null && motors[i].activeSelf)
            {
                currentIndex = i;
                return;
            }
        }
    }

    private void PrepareMotorsForMenu()
    {
        if (motors == null) return;

        for (int i = 0; i < motors.Length; i++)
        {
            if (motors[i] == null) continue;

            Rigidbody2D[] rigidbodies = motors[i].GetComponentsInChildren<Rigidbody2D>(true);
            for (int j = 0; j < rigidbodies.Length; j++)
            {
                rigidbodies[j].simulated = false;
            }
        }
    }

    private void PlaceCurrentMotorInCamera()
    {
        if (motors == null || motors.Length == 0) return;

        Camera camera = Camera.main;
        if (camera == null) return;

        GameObject motor = motors[Mathf.Clamp(currentIndex, 0, motors.Length - 1)];
        if (motor == null || !motor.activeInHierarchy) return;
        if (!TryGetRendererBounds(motor, out Bounds bounds)) return;

        Vector3 target = camera.ViewportToWorldPoint(new Vector3(
            previewViewportPosition.x,
            previewViewportPosition.y,
            Mathf.Abs(camera.transform.position.z - bounds.center.z)));
        target.z = bounds.center.z;

        motor.transform.position += target - bounds.center;
    }

    private bool TryGetRendererBounds(GameObject motor, out Bounds bounds)
    {
        Renderer[] renderers = motor.GetComponentsInChildren<Renderer>(true);
        bounds = new Bounds(motor.transform.position, Vector3.zero);
        bool hasBounds = false;

        for (int i = 0; i < renderers.Length; i++)
        {
            if (!renderers[i].enabled) continue;

            if (hasBounds)
            {
                bounds.Encapsulate(renderers[i].bounds);
            }
            else
            {
                bounds = renderers[i].bounds;
                hasBounds = true;
            }
        }

        return hasBounds;
    }
}
