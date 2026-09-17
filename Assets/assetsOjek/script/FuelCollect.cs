using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FuelPickup : MonoBehaviour
{
    public float fuelAmount = 20f; // Jumlah bensin yang ditambahkan
    public float lifeTime = 10f; // Waktu hidup bensin (dalam detik)

    void Start()
    {
        // Hancurkan bensin setelah waktu hidup habis
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Pastikan pemain memiliki tag "Player"
        {
            // Dapatkan komponen FuelSystem dari pemain
            FuelSystem fuelSystem = other.GetComponent<FuelSystem>();
            if (fuelSystem != null)
            {
                // Tambahkan bensin
                fuelSystem.AddFuel(fuelAmount);

                // Hancurkan item bensin
                Destroy(gameObject);
            }
        }
    }
}