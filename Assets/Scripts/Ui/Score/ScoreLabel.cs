

using System;
using TMPro;
using UnityEngine;

public class ScoreLabel : MonoBehaviour
{
	[SerializeField] private TMP_Text _label;

	public void Awake()
	{
		if(ScoreController.Instance == null)
		{
			Debug.LogError("ScoreController is not on scene");
			_label.enabled = false;
			return;
		}
		ScoreController.Instance.OnScoreChanged.AddListener(OnScoreChangedHandler);

		OnScoreChangedHandler(ScoreController.Instance.Score);
	}

    private void OnScoreChangedHandler(long score)
    {
        _label.text = $"Score: {score}";
    }
}