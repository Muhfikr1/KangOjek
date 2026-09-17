using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 2f, -10f); // Jarak lebih jauh
    public float smoothSpeed = 5f;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void FixedUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
public class CameraResize : MonoBehaviour
{
    public float defaultWidth = 1920f;
    public float defaultOrthoSize = 5f;

    void Start()
    {
        float aspectRatio = (float)Screen.width / (float)Screen.height;
        float orthographicSize = defaultOrthoSize * (defaultWidth / Screen.width) * (9f / aspectRatio);
        Camera.main.orthographicSize = orthographicSize;
    }
}