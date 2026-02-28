using UnityEngine;

public class MageAttack : MonoBehaviour,IPlayerAttack
{
    [SerializeField] private PlayerProjectile projectilePrefab;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float projectileSize = 0.5f;
    [SerializeField] private float damage = 15f;
    [SerializeField] private float attackRate = 0.5f;

     public float ProjectileSpeed => projectileSpeed;
     public float ProjectileSize => projectileSize;
     public float Damage => damage;
     public float AttackRate => attackRate;

    private float nextAttackTime;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Time.time >= nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + attackRate;
            }
        }
    }
    public void Attack()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;

        GameObject projectile = Instantiate(projectilePrefab.gameObject, transform.position,Quaternion.identity);

        projectile.GetComponent<PlayerProjectile>().SetProjectileStats(damage,projectileSize);

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        rb.linearVelocity = direction * projectileSpeed;
    }

    public void AddDamage(float Damage)
    {
        damage += Damage;
    }
    public void AddProjectileSpeed(float ProjectileSpeed)
    {
        projectileSpeed += ProjectileSpeed;
    }
    public void AddProjectileSize(float ProjectileSize)
    {
        projectileSize += ProjectileSize;
    }
    public void AddAttackRate(float AttackRate)
    {
        attackRate -= AttackRate;

        if (attackRate < 0.05f)
            attackRate = 0.05f;
    }
}
