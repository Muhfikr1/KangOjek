using System.Collections;
using UnityEngine;

public class DriverDeathFromHead : MonoBehaviour
{
    public GameObject explosionPrefab; // Prefab ledakan
    public AudioClip explosionSound; // Suara ledakan
    private bool isDead = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDead && collision.gameObject.CompareTag("Ground"))
        {
            isDead = true;
            StartCoroutine(HandleDeath()); // Jalankan Coroutine untuk animasi dan suara sebelum Game Over
        }
    }

    private IEnumerator HandleDeath()
    {
        // Tampilkan animasi ledakan
        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosion, 2f); // Hapus ledakan setelah 2 detik
        }

        // Mainkan suara ledakan
        if (explosionSound != null)
        {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position, 2f);
        }

        // Nonaktifkan player agar tidak bisa bergerak
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(1.5f); // Tunggu animasi dan suara selesai sebelum game over

        // Panggil Game Over
        GameManager.instance.GameOver();
    }
}
