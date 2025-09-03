using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnenmiesController : MonoBehaviour
{

    #region Singleton
    private static EnenmiesController _instance;
    public static EnenmiesController Instance
    {
        get
        {
            if (_instance == null) _instance = FindFirstObjectByType<EnenmiesController>();
            return _instance;
        }
        set => _instance = value;
    }

    #endregion

    [SerializeField] private List<Sprite> AllEnemies;
    [SerializeField] private List<SpawnPoint> SpawnPoints;
    [SerializeField] private List<GameObject> EnemyPrefabList;

    [HideInInspector] public List<IEnemy> enemies = new();

    private int _maxEnemies = 3;
    private int _currentEnemies = 0;
    private bool selectOnNextSpawn = false;

    private void Awake()
    {
        ConfigureEnemiesController();

        Instance = this;
    }

    private void Start()
    {
        SpawnEnemies();
    }

    private void OnEnable()
    {
        AttachListeners();
    }

    private void OnDisable()
    {
        DettachListeners();
    }

    private void AttachListeners()
    {
        GameEvents.EnemyKilled += EnemyKilled;
    }

    private void DettachListeners()
    {
        GameEvents.EnemyKilled -= EnemyKilled;
    }

    private void EnemyKilled(IEnemy enemy)
    {
        FreeSpawnPoint(enemy.GetEnemyPosition());
        enemies.Remove(enemy);
        DestroyKilledEnemy(enemy.GetEnemyObject());
        StartCoroutine(SpawnEnemyViaCor());
        if (enemies.Count > 0)
            SelectEnemy();
        else
            selectOnNextSpawn = true;
    }

    private void SpawnEnemies()
    {
        while (_currentEnemies < _maxEnemies)
        {
            SpawnEnemy();
        }

        SelectEnemy();
    }

    public void SelectEnemy()
    {
        enemies[0].SelectCombat();
    }

    private IEnumerator SpawnEnemyViaCor()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        if (_currentEnemies >= _maxEnemies)
        {
            Debug.LogError("Max Enemies reached! Kil some to spawn new");
            return;
        }

        int freeSpawnPointIndex = -1;
        for (int i = 0; i < SpawnPoints.Count; i++)
        {
            if (SpawnPoints[i].IsOccupied) continue;

            freeSpawnPointIndex = i;
            break;
        }

        if (freeSpawnPointIndex == -1) return;

        SpawnPoints[freeSpawnPointIndex].IsOccupied = true;
        SoulEnemy enemy = Instantiate(EnemyPrefabList[Random.Range(0, EnemyPrefabList.Count)], SpawnPoints[freeSpawnPointIndex].Position.position, Quaternion.identity, transform).GetComponent<SoulEnemy>();
        int spriteIndex = Random.Range(0, AllEnemies.Count);
        enemy.SetupEnemy(AllEnemies[spriteIndex], SpawnPoints[freeSpawnPointIndex]);
        _currentEnemies++;
        enemies.Add(enemy);

        if (selectOnNextSpawn)
        {
            enemy.SelectCombat();
            selectOnNextSpawn = false;
        }
    }

    private void DestroyKilledEnemy(GameObject enemy)
    {
        Destroy(enemy);
    }

    private void FreeSpawnPoint(SpawnPoint spawnPoint)
    {
        for (int i = 0; i < SpawnPoints.Count; i++)
        {
            if (spawnPoint != SpawnPoints[i]) continue; // ??? is spawnPoint not a reference already?

            SpawnPoints[i].IsOccupied = false;
            _currentEnemies--;
            break;
        }
    }

    private void ConfigureEnemiesController()
    {
        _maxEnemies = SpawnPoints != null ? SpawnPoints.Count : 3;
    }

}

[System.Serializable]
public class SpawnPoint
{
    public Transform Position;
    public bool IsOccupied;
}