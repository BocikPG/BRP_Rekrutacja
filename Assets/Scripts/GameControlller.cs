using System;
using UnityEngine;
using UnityEngine.Events;

public enum GameLocalization
{
    SWAMPS,
    DUNGEON,
    CASTLE,
    CITY,
    TOWER
}

public class GameControlller : MonoBehaviour
{
    #region Singleton

    private static GameControlller _instance;

    public static GameControlller Instance
    {
        get
        {
            if (_instance == null) _instance = FindFirstObjectByType<GameControlller>();
            return _instance;
        }
        set => _instance = value;
    }

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    public UnityEvent OnPaused = new();
    public UnityEvent OnUnPaused = new();

    [SerializeField] private GameLocalization currentGameLocalization;

    public GameLocalization CurrentGameLocalization
    {
        get => currentGameLocalization;

        set => currentGameLocalization = value;
    }

    private bool _isPaused;

    public bool IsPaused
    {
        get => _isPaused;
    }

    public bool IsCurrentLocalization(GameLocalization localization)
    {
        return CurrentGameLocalization == localization;
    }

    internal void Pause()
    {
        _isPaused = true;
        Time.timeScale = 0f;

        OnPaused.Invoke();
    }

    internal void UnPause()
    {
        _isPaused = false;
        Time.timeScale = 1f;

        OnUnPaused.Invoke();
    }
}