using UnityEngine;
using UnityEngine.UI;
using GameAnalyticsSDK;

public class GAInitializer : MonoBehaviour
{
    [SerializeField] private Button _sendErrorButton;

    private void Awake()
    {
        GameAnalytics.Initialize();
        Debug.Log("[GA] Initialized");

        if (_sendErrorButton != null)
            _sendErrorButton.onClick.AddListener(TriggerTestError);
    }

    public void TriggerTestError()
    {
        AnalyticsService.Error("manual_test_error", GAErrorSeverity.Warning);
    }
}
