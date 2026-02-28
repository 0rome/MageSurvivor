using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StatsUpgrade : MonoBehaviour, IPlayerUpgrade
{
    [Inject] private Player player;
    private PlayerHealth playerHealth;
    private PlayerMovement playerMovement;

    private void Start()
    {
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    public List<UpgradeOption> GetUpgrades()
    {
        return new List<UpgradeOption>
        {
            new UpgradeOption
            {
                Title = "Increase Max Health",
                Description = "Increases the player's maximum health by 25 points.",
                Apply = () => playerHealth.AddMaxHp(25)
            },
            new UpgradeOption
            {
                Title = "Increase Move Speed",
                Description = "Increases the player's move speed by 2 points",
                Apply = () => playerMovement.AddMoveSpeed(2f)
            },
        };
    }
}
