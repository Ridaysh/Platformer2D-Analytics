using UnityEngine;
using GameAnalyticsSDK;

public static class AnalyticsService
{
    public static void LevelStart(string world, string level)
    {
        Debug.Log($"[GA] Progression START {world}:{level}");
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, world, level);
    }

    public static void LevelComplete(string world, string level, int score = 0)
    {
        Debug.Log($"[GA] Progression COMPLETE {world}:{level} score={score}");
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, world, level, score);
    }

    public static void LevelFail(string world, string level, int score = 0)
    {
        Debug.Log($"[GA] Progression FAIL {world}:{level} score={score}");
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, world, level, score);
    }

    public static void Design(string eventName, float value = 0f)
    {
        Debug.Log($"[GA] Design {eventName} value={value}");
        GameAnalytics.NewDesignEvent(eventName, value);
    }

    public static void GoldGained(float amount, string itemType = "Gameplay", string itemId = "coins_pickup")
    {
        Debug.Log($"[GA] Resource SOURCE Gold +{amount} {itemType}:{itemId}");
        GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Gold", amount, itemType, itemId);
    }

    public static void GoldSpent(float amount, string itemType = "Shop", string itemId = "buy_item")
    {
        Debug.Log($"[GA] Resource SINK Gold -{amount} {itemType}:{itemId}");
        GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Gold", amount, itemType, itemId);
    }

    public static void Error(string message, GAErrorSeverity severity = GAErrorSeverity.Warning)
    {
        Debug.LogWarning($"[GA] Error {severity}: {message}");
        GameAnalytics.NewErrorEvent(severity, message);
    }
}
