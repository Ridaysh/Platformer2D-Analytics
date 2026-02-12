using System;
using UnityEngine;

public class EnemyHealthController : HealthController
{
    public static event Action EnemyKilled;
    [SerializeField] private string _enemyType = "default";

    protected override void Die()
    {
        AudioManager.PlaySound("EnemyDie");
        EnemyKilled?.Invoke();
        AnalyticsService.Design($"enemy:kill:{_enemyType}");
        Destroy(gameObject);
    }

    protected override void OnDamage()
    {
        AudioManager.PlaySound("EnemyHit");
    }
}
