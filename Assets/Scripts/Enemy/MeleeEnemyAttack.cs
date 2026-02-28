using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using UnityEngine;

public class MeleeEnemyAttack : EnemyAttack
{
    [SerializeField] private float damage = 15f;
    [SerializeField] private float attackRadius = 1f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float attackCooldown = 1f;

    private bool isAttacking;

    void Update()
    {
        DetectPlayer();
    }

    private void DetectPlayer()
    {
        if (isAttacking) return;

        if ((player.transform.position - transform.position).sqrMagnitude <= attackRadius * attackRadius)
        {
            isAttacking = true;
            enemyAnimation.AttackAnimation();
            Attack();
        }
    }

    public override void Attack()
    {
        if (health.currentHealth <= 0) return;

        AttackDelay().Forget();
    }

    private async UniTaskVoid AttackDelay()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.25));

        if ((player.transform.position - transform.position).sqrMagnitude <= attackRadius * attackRadius)
            playerHealth.GetDamage(damage);

        await UniTask.Delay(TimeSpan.FromSeconds(attackCooldown));
        isAttacking = false;
    }
}
