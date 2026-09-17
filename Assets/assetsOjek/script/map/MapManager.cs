using UnityEngine;

public class MapManager : MonoBehaviour
{
    private const string PurchasedKey = "MapPurchased_"; // Prefix untuk penyimpanan map

    // Mengecek apakah map sudah terbuka (map 0 selalu terbuka)
    public static bool IsMapUnlocked(int mapID)
    {
        if (mapID == 0) return true; // Map default selalu tersedia
        return PlayerPrefs.GetInt(PurchasedKey + mapID, 0) == 1; // 1 = Sudah dibeli, 0 = Belum
    }

    // Membuka map jika dibeli
    public static void UnlockMap(int mapID)
    {
        PlayerPrefs.SetInt(PurchasedKey + mapID, 1); // Tandai map sebagai terbeli
        PlayerPrefs.Save();
    }
}
