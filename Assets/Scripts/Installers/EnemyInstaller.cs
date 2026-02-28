using UnityEngine;
using Zenject;

public class EnemyInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<EnemyHealth>()
            .FromComponentOnRoot()
            .AsSingle();
        Container.Bind<EnemyAnimation>()
            .FromComponentOnRoot()
            .AsSingle();
    }
}
