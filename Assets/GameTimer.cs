using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextMeshProUGUI _gameOverTimerText; // UI untuk Game Over
    private float _elapsedTime = 0f;
    private bool _isGameOver = false;

    private void Update()
    {
        if (!_isGameOver)
        {
            _elapsedTime += Time.deltaTime;
            UpdateTimerUI();
        }
    }

    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(_elapsedTime / 60);
        int seconds = Mathf.FloorToInt(_elapsedTime % 60);
        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StopTimer()
    {
        _isGameOver = true;
        ShowFinalTime();
    }

    private void ShowFinalTime()
    {
        int minutes = Mathf.FloorToInt(_elapsedTime / 60);
        int seconds = Mathf.FloorToInt(_elapsedTime % 60);
        _gameOverTimerText.text = "Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
