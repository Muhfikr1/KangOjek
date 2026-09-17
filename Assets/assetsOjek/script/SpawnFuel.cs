using UnityEngine;

public class SpawnFuel : MonoBehaviour
{
    public GameObject fuelPrefab; // Prefab bensin
    public float spawnInterval = 10f; // Jarak waktu antara spawn bensin
    public float minX = -5f, maxX = 5f; // Rentang posisi X
    public float minY = 1f, maxY = 3f; // Rentang posisi Y

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        // Spawn bensin setiap interval waktu
        if (timer >= spawnInterval)
        {
            SpawnRandomFuel();
            timer = 0f;
        }
    }

    void SpawnRandomFuel()
    {
        // Tentukan posisi acak
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0);

        // Buat bensin baru
        Instantiate(fuelPrefab, spawnPosition, Quaternion.identity);
    }
}