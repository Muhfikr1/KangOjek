using UnityEngine;
using UnityEngine.UI;

public class FuelSystem : MonoBehaviour
{
    public float maxFuel = 100f; // Kapasitas maksimal bensin
    public float fuelConsumptionRate = 1f; // Pengurangan bensin per detik
    public Slider fuelSlider; // UI Slider untuk menampilkan level bensin
    public GameObject gameOverPanel; // Panel game over

    private float currentFuel;

    void Start()
    {
        currentFuel = maxFuel;
        UpdateFuelUI();
    }

    void Update()
    {
        if (currentFuel > 0)
        {
            // Kurangi bensin seiring waktu
            currentFuel -= fuelConsumptionRate * Time.deltaTime;
            UpdateFuelUI();
        }
        else
        {
            // Bensin habis, tampilkan game over
            GameOver();
        }
    }

    public void AddFuel(float amount)
    {
        currentFuel += amount;
        if (currentFuel > maxFuel)
        {
            currentFuel = maxFuel;
        }
        UpdateFuelUI();
    }

    void UpdateFuelUI()
    {
        fuelSlider.value = currentFuel / maxFuel;
    }

    void GameOver()
    {
        // Tampilkan panel game over
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        // Hentikan permainan
        Time.timeScale = 0f;
    }
}