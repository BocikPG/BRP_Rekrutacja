using UnityEngine;

public class SoulEnemy : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject InteractionPanelObject;
    [SerializeField] private GameObject ActionsPanelObject;
    [SerializeField] private SpriteRenderer EnemySpriteRenderer;
    [SerializeField] private long ScoreAfterKill;
    [SerializeField] private WeaponEnum Weakness;

    private SpawnPoint _enemyPosition;

    public long Score { get => ScoreAfterKill; }
    public bool WasKilledWithWeakness { get; set; }

    public void SetupEnemy(Sprite sprite, SpawnPoint spawnPoint)
    {
        EnemySpriteRenderer.sprite = sprite;
        _enemyPosition = spawnPoint;
        gameObject.SetActive(true);
    }

    public SpawnPoint GetEnemyPosition()
    {
        return _enemyPosition;
    }

    public GameObject GetEnemyObject()
    {
        return this.gameObject;
    }

    private void ActiveCombatWithEnemy()
    {
        ActiveInteractionPanel(false);
        ActiveActionPanel(true);
    }

    private void ActiveInteractionPanel(bool active)
    {
        InteractionPanelObject.SetActive(active);
    }

    private void ActiveActionPanel(bool active)
    {
        ActionsPanelObject.SetActive(active);
    }

    private void UseWeapon(WeaponEnum weapon)
    {
        if(Weakness == weapon) WasKilledWithWeakness = true;
        else WasKilledWithWeakness = false;

        GameEvents.EnemyKilled?.Invoke(this);
    }

    #region OnClicks

    public void Combat_OnClick()
    {
        ActiveCombatWithEnemy();
    }

    public void Bow_OnClick()
    {
        UseWeapon(WeaponEnum.BOW);
    }

    public void Sword_OnClick()
    {
        UseWeapon(WeaponEnum.SWORD);
    }

    #endregion
}

enum WeaponEnum
{
    NONE,
    BOW,
    SWORD,
}

public interface IEnemy
{
    SpawnPoint GetEnemyPosition();
    GameObject GetEnemyObject();
    long Score { get;}
    bool WasKilledWithWeakness { get; set; }
}
