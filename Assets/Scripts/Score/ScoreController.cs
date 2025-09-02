

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
		GameEvents.EnemyKilled += OnEnemyKilled;
		GameEvents.SoulUsed += UseSoul;
	}

	#endregion

	private void UseSoul(SoulItem soul)
	{
		Score += soul.ScoreAfterUse;
		OnScoreChanged.Invoke(Score);
	}


	[HideInInspector] public UnityEvent<long> OnScoreChanged = new();
	public float KillingWithWeaknessScoreMultiplayer = 1.5f;

	public long Score = 0;

	private void OnEnemyKilled(IEnemy enemy)
	{
		Score += calculateScoreFromIEnemy(enemy);
		OnScoreChanged.Invoke(Score);
	}

	public long calculateScoreFromIEnemy(IEnemy enemy)
	{
		if (enemy.WasKilledWithWeakness)
			return (long)(enemy.Score * KillingWithWeaknessScoreMultiplayer);
		else 
			return enemy.Score;
	}


}