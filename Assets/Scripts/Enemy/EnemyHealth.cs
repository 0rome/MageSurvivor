using System;
using System.Drawing;
using UnityEngine;

public class EnemyHealth : Enemy, IEnemyTarget
{
    [SerializeField] private float maxHealth = 50;
    [SerializeField] private SpriteRenderer healthBarSprite;

    public float currentHealth { get; private set; }
    public bool IsDead => currentHealth <= 0;

    public event Action OnHeal;
    public event Action OnDamaged;
    public event Action<EnemyHealth> OnDeath;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void GetDamage(float damage)
    {
        if (currentHealth <= 0)
        { 
            currentHealth = 0;
            return;
        }

        currentHealth -= damage;
        OnDamaged?.Invoke();
        SetHealthbarValue(new Vector3(-damage / maxHealth, 0, 0));

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
        SetHealthbarValue(new Vector3(heal / maxHealth, 0, 0));
    }
    private void Die()
    {
        healthBarSprite.transform.localScale = Vector3.zero;
        Destroy(gameObject,3f);
        OnDeath?.Invoke(this);
    }
    private void SetHealthbarValue(Vector3 size)
    {
        healthBarSprite.transform.localScale += size;
    }
}
