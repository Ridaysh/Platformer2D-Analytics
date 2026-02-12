using System;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public event Action<int> GoldChanged;

    [SerializeField] private int _gold;

    private const string GoldKey = "PlayerGold";

    public int CurrentGold => _gold;

    public bool CanAfford(int amount) => _gold >= amount;

    private void Awake()
    {
        if (PlayerPrefs.HasKey(GoldKey))
            _gold = PlayerPrefs.GetInt(GoldKey);
    }

    private void OnEnable()
    {
        EnemyHealthController.EnemyKilled += HandleEnemyKilled;
        GameController.OnWin += HandleWin;
    }

    private void Start()
    {
        GoldChanged?.Invoke(_gold);
    }

    private void OnDisable()
    {
        EnemyHealthController.EnemyKilled -= HandleEnemyKilled;
        GameController.OnWin -= HandleWin;
    }

    private void HandleEnemyKilled()
    {
        AddGold(1, "Gameplay", "enemy_kill");
    }

    private void HandleWin()
    {
        AddGold(3, "Gameplay", "win_reward");
    }

    private void AddGold(int amount, string itemType, string itemId)
    {
        if (amount <= 0)
            return;

        _gold += amount;
        GoldChanged?.Invoke(_gold);
        AnalyticsService.GoldGained(amount, itemType, itemId);
        SaveGold();
    }

    public bool TrySpend(int amount, string itemType = "Shop", string itemId = "buy_item")
    {
        if (amount <= 0)
            return true;

        if (_gold < amount)
            return false;

        _gold -= amount;
        GoldChanged?.Invoke(_gold);
        AnalyticsService.GoldSpent(amount, itemType, itemId);
        SaveGold();
        return true;
    }

    private void SaveGold()
    {
        PlayerPrefs.SetInt(GoldKey, _gold);
        PlayerPrefs.Save();
    }
}
