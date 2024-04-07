using UnityEngine;

public class PlayerBullet : Bullet
{
    protected override int CollisionMask => 1 << LayerMask.NameToLayer("Enemy");

    protected override void Awake()
    {
        base.Awake();
        MoveSpeed = 20f;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
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