using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;
    private PlayerHealth playerHealth;
    private Rigidbody2D rb;

    private float currentSpeed;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerHealth = GetComponent<PlayerHealth>();

        playerHealth.OnDamaged += HitAnimation;
        playerHealth.OnDeath += DeathAnimation;
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
    public void DeathAnimation()
    {
        animator.SetTrigger("Death");
    }


    private void OnDisable()
    {
        if (playerHealth != null)
        {
            playerHealth.OnDamaged -= HitAnimation;
            playerHealth.OnDeath -= DeathAnimation;
        }
    }
}