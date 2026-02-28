using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, ITarget
{
    [SerializeField] private float maxHealth = 100;

    private float currentHealth;

    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;

    public event Action OnHeal;
    public event Action OnDamaged;
    public event Action OnDeath;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void GetDamage(float damage)
    {
        currentHealth -= damage;
        OnDamaged?.Invoke();

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }
    public void GetHeal(float heal)
    {
        currentHealth += heal;
        OnHeal?.Invoke();
    }
    private void Die()
    {
        Debug.Log("Death");
        OnDeath?.Invoke();
    }
    public void AddMaxHp(float hp)
    {
        maxHealth += hp;
    }
}
