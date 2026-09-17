using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    
    [SerializeField] private int[] mapPrices = { 0, 100, 200, 300, 500 }; // Harga tiap map
    [SerializeField] private GameObject[] buyButtons; // Tombol beli
    [SerializeField] private GameObject[] playButtons; // Tombol play
    [SerializeField] private TextMeshProUGUI coinText; // UI koin di Shop

    private void Start()
    {
        UpdateShopUI();
    }

    private void UpdateShopUI()
    {
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        coinText.text = "" + totalCoins;

        for (int i = 0; i < mapPrices.Length; i++)
        {
            if (MapManager.IsMapUnlocked(i))
            {
                buyButtons[i].SetActive(false);
                playButtons[i].SetActive(true);
            }
            else
            {
                buyButtons[i].SetActive(true);
                playButtons[i].SetActive(false);
            }
        }
    }

    public void BuyMap(int mapID)
    {
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        int price = mapPrices[mapID];

        if (totalCoins >= price)
        {
            totalCoins -= price;
            PlayerPrefs.SetInt("TotalCoins", totalCoins);
            MapManager.UnlockMap(mapID);
            UpdateShopUI();
        }
        else
        {
            Debug.Log("Koin tidak cukup!");
        }
    }
}
