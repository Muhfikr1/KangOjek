using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Untuk restart scene

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel; 
    public Button pauseButton;
    public Button resumeButton;
    public Button restartButton; 
    public Button xButton;

    private bool isPaused = false;

    void Start()
    {
        pauseButton.onClick.AddListener(TogglePause);
        resumeButton.onClick.AddListener(TogglePause);
        restartButton.onClick.AddListener(RestartGame); 
        xButton.onClick.AddListener(TogglePause);
        pausePanel.SetActive(false);
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pausePanel.SetActive(false);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Pastikan waktu berjalan kembali
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }
}
