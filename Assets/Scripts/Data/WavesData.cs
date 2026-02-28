using UnityEngine;

[CreateAssetMenu(fileName = "NewWaveData", menuName = "EnemyWaves/WaveData")]
public class WaveData : ScriptableObject
{
    public string waveName = "New Wave";
    public float duration = 60f;
    public float spawnInterval = 0.5f;
    public int enemiesPerSpawn = 3;
    public int maxEnemyesOnScreen = 50;

    [Header("Enemy Composition")]
    public GameObject[] enemyPrefabs;

    [Header("Difficulty Scaling")]
    public int additionalEnemiesPerWave = 0;
}