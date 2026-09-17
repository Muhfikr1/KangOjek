using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class HeadCollision : MonoBehaviour
{
    public GameObject gameOverPanel; // Panel game over

    private void OnColisionEnter2D(Collision2D collision)
    {
        // Cek jika bersentuhan dengan tanah (gunakan tag atau layer)
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Panggil fungsi game over
            GameOver();
        }
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