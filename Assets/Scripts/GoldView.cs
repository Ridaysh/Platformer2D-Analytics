using TMPro;
using UnityEngine;

public class GoldView : MonoBehaviour
{
    [SerializeField] private TMP_Text _goldText;
    [SerializeField] private GoldManager _goldManager;


    private void OnEnable()
    {
        if (_goldManager == null)
            return;

        _goldManager.GoldChanged += UpdateText;
        UpdateText(_goldManager.CurrentGold);
    }

    private void OnDisable()
    {
        if (_goldManager == null)
            return;

        _goldManager.GoldChanged -= UpdateText;
    }

    private void UpdateText(int gold)
    {
        if (_goldText == null)
            return;

        _goldText.text = $"Gold : {gold}";
    }
}
