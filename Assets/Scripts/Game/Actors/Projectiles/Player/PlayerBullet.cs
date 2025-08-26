using System.Collections;
using UnityEngine;

public class PlayerBullet : Bullet
{
    protected override int CollisionMask => 1 << LayerMask.NameToLayer("Enemy");

    [SerializeField] FloatObject bulletSpeed;

    protected override void Awake()
    {
        base.Awake();

        MoveSpeed = bulletSpeed.value;
        playerShip.LoseLifeAction += OnPlayerLoseLife;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        moveDirection = transform.up;
    }

    protected override void Update()
    {
        base.Update();

        CheckCollisionWith<CommonEnemy>();
        CheckCollisionWith<Boss>();
    }

    void OnPlayerLoseLife()
    {
        Destroy();
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