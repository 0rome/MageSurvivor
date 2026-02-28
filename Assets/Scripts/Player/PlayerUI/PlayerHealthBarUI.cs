using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerHealthBarUI : MonoBehaviour
{
    [SerializeField] private Slider healthbarSlider;

    [Inject] private PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth.OnHeal += UpdateHealthBar;
        playerHealth.OnDamaged += UpdateHealthBar;

        healthbarSlider.maxValue = playerHealth.MaxHealth;
        healthbarSlider.value = playerHealth.MaxHealth;

    }

    private void UpdateHealthBar()
    {
        healthbarSlider.value = playerHealth.CurrentHealth;
    }

    private void OnDisable()
    {
        playerHealth.OnHeal -= UpdateHealthBar;
        playerHealth.OnDamaged -= UpdateHealthBar;
    }
}
