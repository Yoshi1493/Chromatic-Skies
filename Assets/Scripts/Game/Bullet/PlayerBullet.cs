public class PlayerBullet : Bullet
{
    protected override void Update()
    {
        base.Update();
        CheckCollisionWith<Enemy>();
    }

    public override void Destroy()
    {
        PlayerBulletPool.Instance.ReturnToPool(0, this);
    }
}