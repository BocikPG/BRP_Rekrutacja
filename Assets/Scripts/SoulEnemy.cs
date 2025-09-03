using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoulEnemy : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject InteractionCanvas;
    [SerializeField] private GameObject InteractionPanelObject;
    [SerializeField] private GameObject ActionsPanelObject;
    [SerializeField] private GameObject CombatButtonGameObject;
    [SerializeField] private GameObject BowButtonGameObject;
    [SerializeField] private SpriteRenderer EnemySpriteRenderer;

    private SpawnPoint _enemyPosition;

    public void SetupEnemy(Sprite sprite, SpawnPoint spawnPoint)
    {
        GameControlller.Instance.OnPaused.AddListener(() => OnPausedEventHandler());
        GameControlller.Instance.OnUnPaused.AddListener(() => OnUnPausedEventHandler());

        EnemySpriteRenderer.sprite = sprite;
        _enemyPosition = spawnPoint;
        gameObject.SetActive(true);
    }

    private void OnPausedEventHandler()
    {
        InteractionCanvas.SetActive(false);
    }

    private void OnUnPausedEventHandler()
    {
        InteractionCanvas.SetActive(true);
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
        SelectBow();
    }

    private void ActiveInteractionPanel(bool active)
    {
        InteractionPanelObject.SetActive(active);
    }

    private void ActiveActionPanel(bool active)
    {
        ActionsPanelObject.SetActive(active);
    }

    private void UseBow()
    {
        // USE BOW
        GameEvents.EnemyKilled?.Invoke(this);
    }

    private void UseSword()
    {
        GameEvents.EnemyKilled?.Invoke(this);
        // USE SWORD
    }

    #region OnClicks

    public void Combat_OnClick()
    {
        ActiveCombatWithEnemy();
    }

    public void Bow_OnClick()
    {
        UseBow();
    }

    public void Sword_OnClick()
    {
        UseSword();
    }

    public void SelectCombat()
    {
        EventSystem.current.SetSelectedGameObject(CombatButtonGameObject);
    }

    private void SelectBow()
    {
        EventSystem.current.SetSelectedGameObject(BowButtonGameObject);
    }

    #endregion
}


public interface IEnemy
{
    SpawnPoint GetEnemyPosition();
    GameObject GetEnemyObject();
    void SelectCombat();
}
