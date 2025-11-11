using System.Collections;
using UnityEngine;

public class PlayerBullet : Bullet
{
    protected override int CollisionMask => 1 << LayerMask.NameToLayer("Enemy");

    [SerializeField] FloatObject bulletSpeed;

    protected override void Awake()
    {
        base.Awake();
        playerShip.LoseLifeAction += OnPlayerLoseLife;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        MoveSpeed = bulletSpeed.value;
        moveDirection = transform.up;
    }

    protected override void Update()
    {
        base.Update();

        CheckCollisionWith<CommonEnemy>();
        CheckCollisionWith<Boss>();
    }
    protected override IEnumerator Move()
    {
        yield break;
    }

    void OnPlayerLoseLife()
    {
        ReturnToObjectPool();
    }

    public override void Destroy()
    {
        base.Destroy();
        ReturnToObjectPool();
    }

    public override void ReturnToObjectPool()
    {
        PlayerBulletPool.Instance.ReturnToPool(this);
    }
}