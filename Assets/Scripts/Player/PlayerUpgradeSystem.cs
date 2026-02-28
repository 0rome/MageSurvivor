using UnityEngine;
using Zenject;
using System;
using DG.Tweening;

public class PlayerUpgradeSystem : MonoBehaviour
{
    [SerializeField] private GameObject ScreenEffect;

    [Inject] private GameManager gameManager;
    [Inject] private EnemyWavesSystem enemyWavesSystem;

    public event Action Upgrade;


    private void Start()
    {
        enemyWavesSystem.OnWaveEnded += NewUpgrade;
    }
    public void NewUpgrade(int lastWave)
    {
        SetScreenEffectSize(1);
        Upgrade?.Invoke();
    }
    private void OnDisable()
    {
        enemyWavesSystem.OnWaveEnded -= NewUpgrade;
    }
    public void SetScreenEffectSize(float size)
    {
        ScreenEffect.transform.DOScale(new Vector3(size,size,size), 0.5f);
    }
}
