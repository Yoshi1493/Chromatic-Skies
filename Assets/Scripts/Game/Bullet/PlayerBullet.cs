using UnityEngine;

public class PlayerBullet : Bullet
{
    protected override void Update()
    {
        base.Update();
        CheckCollisionWith<Enemy>();
    }

    protected override void CheckCollisionWith<T>()
    {
        Collider2D coll = Physics2D.OverlapCircle(transform.position, 0.32f);

        if (coll)
        {
            coll.GetComponent<T>().TakeDamage(1000);
            Destroy();
        }
    }

    protected override void Destroy()
    {
        PlayerBulletPool.Instance.ReturnToPool(this);
    }
}