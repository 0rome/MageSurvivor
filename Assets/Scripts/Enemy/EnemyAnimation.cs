using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private EnemyHealth enemyHealth;

    private float currentSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();

        enemyHealth.OnDamaged += HitAnimation;
        enemyHealth.OnDeath += DeathAnimation;
    }
    private void Update()
    {
        currentSpeed = rb.linearVelocity.magnitude;

        if (currentSpeed < 0.1f) { WalkingAnimation(false); }
        else { WalkingAnimation(true); }
    }
    public void WalkingAnimation(bool isWalking)
    {
        if (isWalking) { animator.SetBool("isWalking", true); }
        else { animator.SetBool("isWalking", false); }
    }
    public void HitAnimation()
    {
        animator.SetTrigger("GetDamage");
    }
    public void DeathAnimation(EnemyHealth enemyHealth)
    {
        animator.SetTrigger("Death");
    }
    public void AttackAnimation()
    {
        animator.SetTrigger("Attack");
    }


    private void OnDisable()
    {
        enemyHealth.OnDamaged -= HitAnimation;
        enemyHealth.OnDeath -= DeathAnimation;
    }
}
