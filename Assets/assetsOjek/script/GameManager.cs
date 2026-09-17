using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Motor Settings")]
    public GameObject[] motorPrefabs;
    public Transform spawnPoint;

    [Header("UI")]
    public GameObject gameOverPanel;
    public MotorControllerUI motorControllerUI;

    [Header("References")]
    public DisplayDistanceText displayDistanceText;
    public LoopingRoad loopingRoad;
    public CameraFollow cameraFollow;

    private bool isGameOver = false;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        Time.timeScale = 1f;
        gameOverPanel.SetActive(false);

        int selectedMotorID = MotorManager.GetSelectedMotor();
        if (selectedMotorID >= 0 && selectedMotorID < motorPrefabs.Length)
        {
            // Spawn motor
            GameObject motor = Instantiate(motorPrefabs[selectedMotorID], spawnPoint.position, Quaternion.identity);

            // Atur kamera untuk mengikuti motor
            if (cameraFollow != null)
                cameraFollow.SetTarget(motor.transform);

            // Hubungkan UI Controller
            DriverCar driver = motor.GetComponent<DriverCar>();
            if (motorControllerUI != null)
                motorControllerUI.SetDriver(driver);

            // Hubungkan DisplayDistanceText
            if (displayDistanceText != null)
                displayDistanceText.SetTarget(motor.transform);

            // Hubungkan ke LoopingRoad
            if (loopingRoad != null)
                loopingRoad.SetTarget(motor.transform);
        }
    }

    public void GameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;

            AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
            foreach (AudioSource audioSource in audioSources)
            {
            audioSource.Stop();
            }
            
            if (displayDistanceText != null)
                displayDistanceText.SetFinalDistance();

            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;

            GameTimer timer = FindFirstObjectByType<GameTimer>();
            if (timer != null)
                timer.StopTimer();
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }
}
