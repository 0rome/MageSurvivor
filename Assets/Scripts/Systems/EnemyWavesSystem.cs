using System;
using UnityEngine;
using Zenject;

public class EnemyWavesSystem : MonoBehaviour
{
    public event Action<int> OnWaveStarted;
    public event Action<int> OnWaveEnded;
    public event Action<int> OnWaveCountdown;

    [Header("Wave Settings")]
    [SerializeField] private WaveData[] waves;
    [SerializeField] private int timeBetweenWaves = 5;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnOffset = 2f;

    [Inject] private DiContainer container;
    [Inject] private BestWaveAPI bestWaveAPI;

    private enum WaveState
    {
        WaitingForNextWave,
        SpawningWave,
        WaitingForEnemiesToDie,
        Finished
    }

    private WaveState currentState;

    private int currentWaveIndex = 0;
    private int aliveCount = 0;

    private float stateTimer = 0f;
    private float spawnTimer = 0f;
    private int lastCountdownValue = -1;


    public int TimeBetweenWaves => timeBetweenWaves;
    public int CurrentWaveIndex => currentWaveIndex ;

    private void Start()
    {
        ChangeState(WaveState.WaitingForNextWave);
    }

    private void Update()
    {
        switch (currentState)
        {
            case WaveState.WaitingForNextWave:
                UpdateWaitingForNextWave();
                break;

            case WaveState.SpawningWave:
                UpdateSpawningWave();
                break;

            case WaveState.WaitingForEnemiesToDie:
                UpdateWaitingForEnemies();
                break;
        }
    }

    // =========================
    // STATE: WaitingForNextWave
    // =========================

    private void UpdateWaitingForNextWave()
    {
        stateTimer -= Time.deltaTime;

        int currentValue = Mathf.CeilToInt(stateTimer);

        if (currentValue != lastCountdownValue && currentValue >= 0)
        {
            lastCountdownValue = currentValue;
            OnWaveCountdown?.Invoke(currentValue);
        }

        if (stateTimer <= 0f)
        {
            lastCountdownValue = -1;
            StartWave();
        }
    }


    // =========================
    // STATE: SpawningWave
    // =========================

    private void UpdateSpawningWave()
    {
        spawnTimer += Time.deltaTime;
        stateTimer += Time.deltaTime;

        if (currentWaveIndex >= waves.Length)
            return;

        WaveData wave = waves[currentWaveIndex];

        if (spawnTimer >= wave.spawnInterval)
        {
            spawnTimer = 0f;
            SpawnWave(wave);
        }

        if (stateTimer >= wave.duration)
        {
            ChangeState(WaveState.WaitingForEnemiesToDie);
        }
    }

    // =========================
    // STATE: WaitingForEnemiesToDie
    // =========================

    private void UpdateWaitingForEnemies()
    {
        if (aliveCount <= 0)
        {
            currentWaveIndex++;

            if (currentWaveIndex >= waves.Length)
            {
                ChangeState(WaveState.Finished);
                Debug.Log("Все волны пройдены!");
                return;
            }
            OnWaveEnded?.Invoke(currentWaveIndex);
            bestWaveAPI.SendBestWave(UnityEngine.Random.Range(0,9999).ToString(),currentWaveIndex);
            Debug.Log($"Wave {currentWaveIndex} ended!");
            ChangeState(WaveState.WaitingForNextWave);
        }
    }

    // =========================
    // STATE CHANGER
    // =========================

    private void ChangeState(WaveState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case WaveState.WaitingForNextWave:
                stateTimer = timeBetweenWaves;
                break;

            case WaveState.SpawningWave:
                stateTimer = 0f;
                spawnTimer = 0f;
                break;
        }
    }

    private void StartWave()
    {
        Debug.Log($"Wave {currentWaveIndex + 1} started!");

        OnWaveStarted?.Invoke(currentWaveIndex + 1);

        ChangeState(WaveState.SpawningWave);
    }

    private void SpawnWave(WaveData wave)
    {
        if (aliveCount >= wave.maxEnemyesOnScreen)
            return;

        int scaledEnemies = wave.enemiesPerSpawn +
                    (wave.additionalEnemiesPerWave * currentWaveIndex);

        int spawnCount = Mathf.Min(
            scaledEnemies,
            wave.maxEnemyesOnScreen - aliveCount
        );


        for (int i = 0; i < spawnCount; i++)
        {
            SpawnEnemy(wave);
        }


    }

    private void SpawnEnemy(WaveData wave)
    {
        if (wave.enemyPrefabs.Length == 0)
            return;

        GameObject prefab = wave.enemyPrefabs[
            UnityEngine.Random.Range(0, wave.enemyPrefabs.Length)
        ];

        Vector2 spawnPosition = GetRandomSpawnPositionOutsideScreen();

        GameObject enemy = container.InstantiatePrefab(
            prefab,
            spawnPosition,
            Quaternion.identity,
            null
        );

        EnemyHealth health = enemy.GetComponent<EnemyHealth>();

        if (health != null)
            health.OnDeath += HandleEnemyDeath;

        aliveCount++;
    }

    private void HandleEnemyDeath(EnemyHealth health)
    {
        health.OnDeath -= HandleEnemyDeath;
        aliveCount--;
    }

    private Vector2 GetRandomSpawnPositionOutsideScreen()
    {
        if (Camera.main == null)
            return Vector2.zero;

        Vector2 min = Camera.main.ViewportToWorldPoint(Vector2.zero);
        Vector2 max = Camera.main.ViewportToWorldPoint(Vector2.one);

        Vector2 spawnMin = min - Vector2.one * spawnOffset;
        Vector2 spawnMax = max + Vector2.one * spawnOffset;

        int side = UnityEngine.Random.Range(0, 4);

        switch (side)
        {
            case 0:
                return new Vector2(spawnMin.x, UnityEngine.Random.Range(min.y, max.y));
            case 1:
                return new Vector2(spawnMax.x, UnityEngine.Random.Range(min.y, max.y));
            case 2:
                return new Vector2(UnityEngine.Random.Range(min.x, max.x), spawnMax.y);
            default:
                return new Vector2(UnityEngine.Random.Range(min.x, max.x), spawnMin.y);
        }
    }
}
