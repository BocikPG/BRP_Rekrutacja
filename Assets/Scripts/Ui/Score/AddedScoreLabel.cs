using DG.Tweening;
using TMPro;
using UnityEngine;

public class AddedScoreLabel : MonoBehaviour
{
    [SerializeField] private TMP_Text Label;
    [SerializeField] private float EndingXPositionOffset;
    [SerializeField] private float EndingYPositionOffset;
    [SerializeField] private float FloatingDuration = 1f;

    public void Awake()
    {
        GameEvents.EnemyKilled += OnEnemyKilledHandler;
    }

    public void OnEnemyKilledHandler(IEnemy enemy)
    {
        Label.text = $"+{enemy.Score}";

        Vector3 enemyPosition = enemy.GetEnemyPosition().Position.position;
        Vector3 startingPosition = Camera.main.WorldToScreenPoint(enemyPosition);

        Label.transform.position = startingPosition;
        Label.enabled = true;

        Sequence sequence = DOTween.Sequence();

        sequence.Append(Label.transform.DOMove(new Vector3(startingPosition.x + EndingXPositionOffset, startingPosition.y + EndingYPositionOffset, 0), FloatingDuration))
                .AppendCallback(Hide);

    }

    public void Hide()
    {
        Label.enabled = false;
    }
}
