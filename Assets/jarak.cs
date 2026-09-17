using UnityEngine;
using TMPro;

public class DisplayDistanceText : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI _distanceText;         // Teks jarak saat main
    [SerializeField] private TextMeshProUGUI _coinText;             // Teks koin di Game Over
    [SerializeField] private TextMeshProUGUI _gameOverDistanceText; // Teks jarak di Game Over

    [Header("Player Reference")]
    private Transform _playerTransform;
    private Vector2 _startPosition;
    private float _finalDistance = 0f;

    private bool _hasTarget = false;

    // Dipanggil dari GameManager setelah player (motor) di-spawn
    public void SetTarget(Transform player)
    {
        _playerTransform = player;
        _startPosition = player.position;
        _hasTarget = true;
    }

    private void Update()
    {
        if (!_hasTarget || Time.timeScale == 0) return;

        Vector2 distance = (Vector2)_playerTransform.position - _startPosition;
        distance.y = 0f;

        // Cegah nilai negatif
        float distanceX = Mathf.Max(0f, distance.x);

        _distanceText.text = $"{distanceX:F0}m";
    }

    // Dipanggil oleh GameManager saat game over
    public void SetFinalDistance()
    {
        if (!_hasTarget) return;

        _finalDistance = _playerTransform.position.x - _startPosition.x;
        _finalDistance = Mathf.Max(0f, _finalDistance); // Hindari nilai negatif

        int earnedCoins = Mathf.FloorToInt(_finalDistance * 4); // Misal: 1 meter = 4 koin

        // Tampilkan di UI Game Over
        _gameOverDistanceText.text = $"Distance: {_finalDistance:F0}m";
        _coinText.text = $"Coins: {earnedCoins}";

        // Simpan koin ke PlayerPrefs
        int currentCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        int newTotal = currentCoins + earnedCoins;
        PlayerPrefs.SetInt("TotalCoins", newTotal);
        PlayerPrefs.Save();

        Debug.Log($"Final Distance: {_finalDistance:F0}m, Earned Coins: {earnedCoins}, Total Coins: {newTotal}");
    }
}
