using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelection : MonoBehaviour
{
    public void PlayMap(int mapID)
    {
        if (MapManager.IsMapUnlocked(mapID))
        {
            SceneManager.LoadScene("Map_" + mapID); // Pastikan scene bernama "Map_1", "Map_2", dst.
        }
        else
        {
            Debug.Log("Map belum dibeli!");
        }
    }
}
