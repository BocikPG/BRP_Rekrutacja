using DG.Tweening;
using TMPro;
using UnityEditor.EditorTools;
using UnityEngine;

public class AddedScoreLabel : MonoBehaviour
{
    [SerializeField] private TMP_Text Label;
    [SerializeField] private AddedScoreLabelUseCase UseCase;
    [Tooltip("Used only in SOUL USE use case")]
    [SerializeField] private Transform StartingPosition;
    [SerializeField] private float EndingXPositionOffset;
    [SerializeField] private float EndingYPositionOffset;
    [SerializeField] private float FloatingDuration = 1f;

    private Sequence sequence;

    public void Awake()
    {
        if (UseCase == AddedScoreLabelUseCase.EnemyKilled) GameEvents.EnemyKilled += OnEnemyKilledHandler;
        if (UseCase == AddedScoreLabelUseCase.SoulUse) GameEvents.SoulUsed += OnSoulUsedHandler;
    }

    public void OnEnemyKilledHandler(IEnemy enemy)
    {
        sequence.Kill();

        Label.text = $"+{ScoreController.Instance.calculateScoreFromIEnemy(enemy)}";

        Vector3 enemyPosition = enemy.GetEnemyPosition().Position.position;
        Vector3 startingPosition = Camera.main.WorldToScreenPoint(enemyPosition);
        SetUpLabelAndSequence(startingPosition);

    }

    public void OnSoulUsedHandler(SoulItem soul)
    {
        sequence.Kill();

        Label.text = $"+{soul.ScoreAfterUse}";
        Vector3 startingPosition = StartingPosition.position;

        SetUpLabelAndSequence(startingPosition);
    }

    private void SetUpLabelAndSequence(Vector3 startingPosition)
    {
        Label.transform.position = startingPosition;
        Label.enabled = true;

        sequence = DOTween.Sequence();

        sequence.Append(Label.transform.DOMove(new Vector3(startingPosition.x + EndingXPositionOffset, startingPosition.y + EndingYPositionOffset, 0), FloatingDuration))
                .AppendCallback(Hide)
                .SetUpdate(true);
    }

    public void Hide()
    {
        Label.enabled = false;
    }
}

internal enum AddedScoreLabelUseCase
{
    EnemyKilled,
    SoulUse
}
