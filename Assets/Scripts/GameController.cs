using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerHealthController playerHealthController;

    private const string WorldName = "World1";
    private bool _levelEnded;
    
    public static event Action OnGameOver;
    public static event Action OnWin;

    private void Awake()
    {
        if (playerHealthController != null)
        {
            playerHealthController.OnPlayerDied += HandlePlayerDeath;
        }

        Time.timeScale = 1;
    }

    private void Start()
    {
        AnalyticsService.LevelStart(WorldName, SceneManager.GetActiveScene().name);
    }

    private void OnDestroy()
    {
        if (playerHealthController != null)
        {
            playerHealthController.OnPlayerDied -= HandlePlayerDeath;
        }
    }

    public static void TriggerWin()
    {
        OnWin?.Invoke();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void HandlePlayerDeath()
    {
        if (_levelEnded)
            return;

        _levelEnded = true;
        AnalyticsService.LevelFail(WorldName, SceneManager.GetActiveScene().name);
        OnGameOver?.Invoke();
    }

    private void OnEnable()
    {
        OnWin += HandleWin;
    }

    private void OnDisable()
    {
        OnWin -= HandleWin;
    }

    private void HandleWin()
    {
        if (_levelEnded)
            return;

        _levelEnded = true;
        AnalyticsService.LevelComplete(WorldName, SceneManager.GetActiveScene().name);
    }
}
