using System.Collections;
using UnityEngine;

public class PlayerBullet : Bullet
{
    protected override int CollisionMask => 1 << LayerMask.NameToLayer("Boss");

    protected override void Awake()
    {
        base.Awake();
        MoveSpeed = projectileData.Speed.Value;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        moveDirection = transform.up;
    }

    protected override void Update()
    {
        base.Update();
        CheckCollisionWith<Boss>();
    }

    public override void Destroy()
    {
        PlayerBulletPool.Instance.ReturnToPool(this);
    }

    protected override IEnumerator Move()
    {
        yield break;
    }
}