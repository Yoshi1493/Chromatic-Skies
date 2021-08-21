using UnityEngine;

public class PlayerBullet : Bullet
{
    void OnEnable()
    {
        moveDirection = transform.up;
    }

    protected override void Update()
    {
        base.Update();
        CheckCollisionWith<Enemy>();
    }

    public override void Destroy()
    {
        PlayerBulletPool.Instance.ReturnToPool(this);
    }
}