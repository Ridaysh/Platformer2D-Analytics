using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GoldManager _goldManager;
    [SerializeField] private SpriteRenderer _playerRenderer;
    [SerializeField] private int _skinCost = 5;

    private const string PlayerColorKey = "PlayerColorRGBA";

    public int SkinCost => _skinCost;

    private void Awake()
    {
        LoadAndApplyColor();
    }

    public bool TryBuyRandomSkin()
    {
        if (_goldManager == null)
        {
            Debug.LogWarning("ShopManager: GoldManager not found in scene.", this);
            return false;
        }

        if (!_goldManager.TrySpend(_skinCost, "Shop", "random_skin"))
            return false;

        Color color = GetRandomColor();
        ApplyColor(color);
        SaveColor(color);
        AnalyticsService.Design("shop:buy_color", _skinCost);
        return true;
    }

    private Color GetRandomColor() => Random.ColorHSV();

    private void ApplyColor(Color color)
    {
        _playerRenderer.color = color;
    }

    private void SaveColor(Color color)
    {
        string hex = ColorUtility.ToHtmlStringRGBA(color);
        PlayerPrefs.SetString(PlayerColorKey, hex);
        PlayerPrefs.Save();
    }

    private void LoadAndApplyColor()
    {
        if (!PlayerPrefs.HasKey(PlayerColorKey))
            return;

        string hex = PlayerPrefs.GetString(PlayerColorKey);
        if (ColorUtility.TryParseHtmlString("#" + hex, out Color color))
            ApplyColor(color);
    }
}
