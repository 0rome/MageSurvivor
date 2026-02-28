using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using System.Collections.Generic;
using System.Linq;

public class PlayerUpgradeUI : MonoBehaviour
{
    [SerializeField] private GameObject UpgradeCardPrefab;
    [SerializeField] private int cardsToShow = 4;

    [Inject] private PlayerUpgradeSystem upgradeSystem;
    [Inject] private Player player;

    private IPlayerUpgrade[] playerUpgrades;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnCards();
        }
    }

    private void Start()
    {
        playerUpgrades = player.GetComponents<IPlayerUpgrade>();
    }

    private void OnEnable()
    {
        upgradeSystem.Upgrade += SpawnCards;
    }

    private void OnDisable()
    {
        upgradeSystem.Upgrade -= SpawnCards;
    }

    public void SpawnCards()
    {
        ClearOldCards();

        List<UpgradeOption> allUpgrades = new List<UpgradeOption>();

        foreach (var upgradeProvider in playerUpgrades)
        {
            allUpgrades.AddRange(upgradeProvider.GetUpgrades());
        }

        // Берём случайные без повторений
        List<UpgradeOption> randomUpgrades =
            allUpgrades.OrderBy(x => Random.value)
                       .Take(cardsToShow)
                       .ToList();

        foreach (var upgrade in randomUpgrades)
        {
            var card = Instantiate(UpgradeCardPrefab, transform);

            Button button = card.GetComponent<Button>();
            TextMeshProUGUI text = card.GetComponentInChildren<TextMeshProUGUI>();

            text.text = upgrade.Title;

            button.onClick.AddListener(() =>
            {
                upgrade.Apply();
                ClearOldCards();
                upgradeSystem.SetScreenEffectSize(11);
            });
        }
    }
    private void ClearOldCards()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
