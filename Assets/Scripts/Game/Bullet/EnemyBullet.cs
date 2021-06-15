public class EnemyBullet : Bullet
{
    protected override void Update()
    {
        base.Update();
        CheckCollisionWith<Player>();
    }

    protected override void Destroy()
    {
        EnemyBulletPool.Instance.ReturnToPool(0, this);
    }
}