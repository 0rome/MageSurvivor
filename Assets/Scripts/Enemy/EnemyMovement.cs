using UnityEngine;
using Zenject;

public class EnemyMovement : Enemy
{
    [SerializeField] private float speed;
    [SerializeField] private float separationRadius = 0.6f;
    [SerializeField] private float separationForce = 3f;
    [SerializeField] private LayerMask enemyLayer;

    [Inject] private Player player;
    [Inject] private EnemyHealth health;

    private float moveUpdateTimer = 0f;
    private float moveUpdateRate = 0.5f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        moveUpdateTimer -= Time.fixedDeltaTime;
        if (moveUpdateTimer <= 0f)
        {
            Movement();
            moveUpdateTimer = moveUpdateRate;
        }
    }
    private void Movement()
    {
        if (health.currentHealth <= 0)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 toPlayer = player.transform.position - transform.position;

        if (toPlayer.sqrMagnitude <= 1f)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        toPlayer.Normalize();

        ///////////// Separation
        Vector2 separation = Vector2.zero;

        Collider2D[] neighbours = Physics2D.OverlapCircleAll(
            transform.position,
            separationRadius,
            enemyLayer
        );

        foreach (var neighbour in neighbours)
        {
            if (neighbour.attachedRigidbody == rb)
                continue;

            Vector2 diff = (Vector2)(transform.position - neighbour.transform.position);
            float distance = diff.magnitude;

            if (distance > 0)
            {
                separation += diff.normalized / distance;
            }
        }

        Vector2 finalDirection = (toPlayer + separation * separationForce).normalized;

        rb.linearVelocity = finalDirection * speed;

        // Разворот
        if (finalDirection.x > 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }
    
}
