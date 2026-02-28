using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MageUpgrades : MonoBehaviour, IPlayerUpgrade
{
    [Inject] private Player player;

    private MageAttack mageAttack;

    private void Awake()
    {
        mageAttack = GetComponent<MageAttack>();
    }

    public List<UpgradeOption> GetUpgrades()
    {
        return new List<UpgradeOption>
        {
            new UpgradeOption
            {
                Title = "Damage +",
                Description = "Increase spell damage",
                Apply = () => mageAttack.AddDamage(10 * player.PlayerLevel)
            },

            new UpgradeOption
            {
                Title = "Projectile Speed +",
                Description = "Faster projectiles",
                Apply = () =>
                {
                    if (mageAttack.ProjectileSpeed < 100)
                        mageAttack.AddProjectileSpeed(2 * player.PlayerLevel);
                }
            },

            new UpgradeOption
            {
                Title = "Projectile Size +",
                Description = "Bigger projectiles",
                Apply = () =>
                {
                    if (mageAttack.ProjectileSize < 5)
                        mageAttack.AddProjectileSize(0.5f);
                }
            },

            new UpgradeOption
            {
                Title = "Attack Speed +",
                Description = "Cast faster",
                Apply = () =>
                {
                    if (mageAttack.AttackRate > 0.1f)
                        mageAttack.AddAttackRate(0.1f);
                }
            }
        };
    }
}
