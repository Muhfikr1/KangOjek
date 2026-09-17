using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        public Transform layerTransform; // Background layer
        public float parallaxSpeedX = 0.5f; // Speed of horizontal movement
        public float parallaxSpeedY = 0.5f; // Speed of vertical movement
    }

    public ParallaxLayer[] layers; // Array of layers
    public Transform cameraTransform; // Reference to the main camera
    private Vector3 lastCameraPosition;

    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform; // Assign main camera if not set
        }
        lastCameraPosition = cameraTransform.position;
    }

    void LateUpdate()
    {
        Vector3 cameraDelta = cameraTransform.position - lastCameraPosition; // Get camera movement

        foreach (ParallaxLayer layer in layers)
        {
            if (layer.layerTransform != null)
            {
                Vector3 newPosition = layer.layerTransform.position;
                newPosition.x += cameraDelta.x * layer.parallaxSpeedX;
                newPosition.y += cameraDelta.y * layer.parallaxSpeedY;
                layer.layerTransform.position = newPosition;
            }
        }

        lastCameraPosition = cameraTransform.position;
    }
}
