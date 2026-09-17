using UnityEngine;
using TMPro;

public class MainMenuDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _totalCoinsText;
    
    private void Start()
    {
        ConfigureTotalCoinsTextLayout();

        // Mengambil jumlah koin dari PlayerPrefs
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        
        // Menampilkan jumlah koin di UI
        if (_totalCoinsText != null)
        {
            _totalCoinsText.text = totalCoins.ToString();
        }
    }

    private void ConfigureTotalCoinsTextLayout()
    {
        if (_totalCoinsText == null)
        {
            return;
        }

        _totalCoinsText.alignment = TextAlignmentOptions.MidlineLeft;
        _totalCoinsText.enableAutoSizing = true;
        _totalCoinsText.fontSizeMin = 32f;
        _totalCoinsText.fontSizeMax = 65f;
        _totalCoinsText.textWrappingMode = TextWrappingModes.NoWrap;
        _totalCoinsText.overflowMode = TextOverflowModes.Ellipsis;
        _totalCoinsText.raycastTarget = false;
    }
}
