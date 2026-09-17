using UnityEngine;
using UnityEngine.U2D;

[ExecuteInEditMode]
public class EnvironmentGenerator : MonoBehaviour
{
    [SerializeField] private SpriteShapeController _spriteShapeController;

    [SerializeField, Range(3f, 100f)] private int _levelLength = 50;
    [SerializeField, Range(1f, 50f)] private float _xMultiplier = 2f;
    [SerializeField, Range(1f, 50f)] private float _yMultiplier = 2f;
    [SerializeField, Range(0f, 1f)] private float _curveSmoothness = 0.5f;
    [SerializeField] private float _noiseStep = 0.5f;
    [SerializeField] private float _bottom = 10f;
    [SerializeField] private float _noiseSeed = 0f; // Noise seed untuk konsistensi
    [SerializeField] private bool _autoUpdate = true; // Kontrol auto-update

    private void OnValidate()
    {
        if (!_autoUpdate || _spriteShapeController == null)
            return;

        GenerateTerrain();
    }

    public void GenerateTerrain()
    {
        // Reset spline
        _spriteShapeController.spline.Clear();

        // Generate terrain points
        for (int i = 0; i < _levelLength; i++)
        {
            float xPos = i * _xMultiplier;
            float yPos = Mathf.PerlinNoise(i * _noiseStep + _noiseSeed, _noiseSeed) * _yMultiplier;
            Vector3 pointPosition = new Vector3(xPos, yPos); // Gunakan koordinat lokal

            // Insert point
            _spriteShapeController.spline.InsertPointAt(i, pointPosition);

            // Set tangents
            if (i > 0 && i < _levelLength - 1)
            {
                SetSplineTangents(i);
            }
        }

        // Add bottom points
        Vector3 lastPoint = _spriteShapeController.spline.GetPosition(_levelLength - 1);
        _spriteShapeController.spline.InsertPointAt(_levelLength, new Vector3(lastPoint.x, -_bottom));
        _spriteShapeController.spline.InsertPointAt(_levelLength + 1, new Vector3(0, -_bottom));
    }

    private void SetSplineTangents(int index)
    {
        _spriteShapeController.spline.SetTangentMode(index, ShapeTangentMode.Continuous);
        Vector3 tangentOffset = Vector3.right * _xMultiplier * _curveSmoothness;
        _spriteShapeController.spline.SetLeftTangent(index, -tangentOffset);
        _spriteShapeController.spline.SetRightTangent(index, tangentOffset);
    }
}