using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private float Damage;
    private float Size;

    private void Start()
    {
        transform.localScale = new Vector3(Size,Size,Size);
        Destroy(gameObject,3f);
    }
    public void SetProjectileStats(float damage,float size)
    {
        Damage = damage;
        Size = size;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var targets = collision.GetComponents<MonoBehaviour>();

        foreach (var t in targets)
        {
            if (t is IEnemyTarget enemyTarget)
            {
                enemyTarget.GetDamage(Damage);
                //Destroy(gameObject);
                return; // чтобы не бить несколько раз
            }
        }
    }

}
