using Zenject;
using UnityEngine;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private Player player;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private EnemyWavesSystem enemyWavesSystem;
    [SerializeField] private PlayerUpgradeSystem upgradeSystem;
    [SerializeField] private BestWaveAPI bestWaveAPI;

    public override void InstallBindings()
    {
        Container.Bind<Player>()
            .FromInstance(player)
            .AsSingle();
        Container.Bind<PlayerHealth>()
           .FromInstance(player.GetComponent<PlayerHealth>())
           .AsSingle();
        Container.Bind<GameManager>()
           .FromInstance(gameManager)
           .AsSingle();
        Container.Bind<EnemyWavesSystem>()
           .FromInstance(enemyWavesSystem)
           .AsSingle();
        Container.Bind<PlayerUpgradeSystem>()
            .FromInstance(upgradeSystem)
           .AsSingle();
        Container.Bind<BestWaveAPI>()
            .FromInstance(bestWaveAPI)
            .AsSingle();
    }
}
