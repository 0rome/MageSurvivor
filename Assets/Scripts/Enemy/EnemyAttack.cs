using UnityEngine;
using Zenject;

public abstract class EnemyAttack : Enemy
{
    [Inject] protected Player player;
    [Inject] protected PlayerHealth playerHealth;
    [Inject] protected EnemyAnimation enemyAnimation;
    [Inject] protected EnemyHealth health;
    public abstract void Attack();
}
