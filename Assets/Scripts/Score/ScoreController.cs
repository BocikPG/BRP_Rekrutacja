

using UnityEngine;
using UnityEngine.Events;

public class ScoreController : MonoBehaviour
{
	#region Singleton

    private static ScoreController _instance;

    public static ScoreController Instance
    {
        get
        {
            if (_instance == null) _instance = FindFirstObjectByType<ScoreController>();
            return _instance;
        }
        set => _instance = value;
    }

    private void Awake()
    {
        Instance = this;
    }

    #endregion

	[HideInInspector] public UnityEvent<long> OnScoreChanged = new();

	public long Score = 0;

	public void OnEnemyKilled(IEnemy enemy)
	{
		Score += enemy.Score;
		OnScoreChanged.Invoke(Score);
	}


}