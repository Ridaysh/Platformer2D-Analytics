using UnityEngine;
using UnityEngine.UI;

public class ShopView : MonoBehaviour
{
    [SerializeField] private GameObject _shopPanel;
    [SerializeField] private ShopManager _shopManager;
    [SerializeField] private GoldManager _goldManager;
    [SerializeField] private Button _buyColorButton;

    private void Awake()
    {
        if (_shopPanel != null)
            _shopPanel.SetActive(false);

        _buyColorButton.onClick.AddListener(BuyColor);
    }

    private void OnEnable()
    {
        if (_goldManager != null)
            _goldManager.GoldChanged += HandleGoldChanged;

        RefreshBuyState();
    }

    private void OnDisable()
    {
        if (_goldManager != null)
            _goldManager.GoldChanged -= HandleGoldChanged;
    }

    public void OpenShop()
    {
        if (_shopPanel != null)
            _shopPanel.SetActive(true);

        AnalyticsService.Design("shop:open");
        RefreshBuyState();
    }

    public void CloseShop()
    {
        if (_shopPanel != null)
            _shopPanel.SetActive(false);
    }

    public void BuyColor()
    {
        if (_shopManager == null)
            return;

        _shopManager.TryBuyRandomSkin();
        RefreshBuyState();
    }

    private void HandleGoldChanged(int gold)
    {
        RefreshBuyState();
    }

    private void RefreshBuyState()
    {
        if (_buyColorButton == null)
            return;

        if (_goldManager == null || _shopManager == null)
        {
            _buyColorButton.interactable = false;
            return;
        }

        _buyColorButton.interactable = _goldManager.CanAfford(_shopManager.SkinCost);
    }

}
