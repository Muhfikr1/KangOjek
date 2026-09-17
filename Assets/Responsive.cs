using UnityEngine;

public class FitCameraToSprite : MonoBehaviour
{
    public SpriteRenderer target;

    void Start()
    {
        if (target == null) return;

        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = target.bounds.size.x / target.bounds.size.y;

        if (screenRatio >= targetRatio)
        {
            Camera.main.orthographicSize = target.bounds.size.y / 2;
        }
        else
        {
            float differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = target.bounds.size.y / 2 * differenceInSize;
        }
    }
}
