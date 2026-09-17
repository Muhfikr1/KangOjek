using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoad : MonoBehaviour
{
    private const string MainMenuSceneName = "MainMenu";
    private const string MenuMapSceneName = "MenuMap";
    private const string ReturnButtonOverlayName = "ReturnToMainMenuClickArea";

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void RegisterSceneLoadFallbacks()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
        BindMenuMapReturnButton(SceneManager.GetActiveScene());
    }

    public void LoadScene(string sceneName)
    {
        if (string.IsNullOrWhiteSpace(sceneName))
        {
            Debug.LogWarning("SceneLoad.LoadScene dipanggil tanpa nama scene.");
            return;
        }

        SceneManager.LoadScene(sceneName);
    }

    public void LoadMainMenu()
    {
        LoadScene(MainMenuSceneName);
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        BindMenuMapReturnButton(scene);
    }

    private static void BindMenuMapReturnButton(Scene scene)
    {
        if (!string.Equals(scene.name, MenuMapSceneName, StringComparison.Ordinal))
        {
            return;
        }

        Button[] buttons = UnityEngine.Object.FindObjectsByType<Button>(FindObjectsInactive.Include);
        foreach (Button button in buttons)
        {
            if (!IsMenuMapReturnButton(button))
            {
                continue;
            }

            button.interactable = true;
            button.onClick.RemoveListener(LoadMainMenuFromReturnButton);
            button.onClick.AddListener(LoadMainMenuFromReturnButton);

            if (button.targetGraphic != null)
            {
                button.targetGraphic.raycastTarget = true;
            }
        }

        EnsureMenuMapReturnClickArea();
    }

    private static void LoadMainMenuFromReturnButton()
    {
        SceneManager.LoadScene(MainMenuSceneName);
    }

    private static bool IsMenuMapReturnButton(Button button)
    {
        return string.Equals(button.name, "return", StringComparison.OrdinalIgnoreCase)
            || string.Equals(button.name, "Button (3)", StringComparison.OrdinalIgnoreCase);
    }

    private static void EnsureMenuMapReturnClickArea()
    {
        if (GameObject.Find(ReturnButtonOverlayName) != null)
        {
            return;
        }

        GameObject canvasObject = new GameObject(ReturnButtonOverlayName, typeof(RectTransform), typeof(Canvas), typeof(GraphicRaycaster));
        Canvas canvas = canvasObject.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;
        canvas.sortingOrder = short.MaxValue;

        GameObject buttonObject = new GameObject("return", typeof(RectTransform), typeof(Image), typeof(Button));
        buttonObject.transform.SetParent(canvasObject.transform, false);

        RectTransform rectTransform = buttonObject.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(1f, 1f);
        rectTransform.anchorMax = new Vector2(1f, 1f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rectTransform.anchoredPosition = new Vector2(-155f, -155f);
        rectTransform.sizeDelta = new Vector2(260f, 260f);

        Image image = buttonObject.GetComponent<Image>();
        image.color = new Color(1f, 1f, 1f, 0.001f);
        image.raycastTarget = true;

        Button button = buttonObject.GetComponent<Button>();
        button.onClick.AddListener(LoadMainMenuFromReturnButton);
    }
}
