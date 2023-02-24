using System.Collections;
using UnityEngine;

public abstract class EnemyBullet : Bullet
{
    protected Enemy ownerShip;
    protected Player playerShip;

    protected override int CollisionMask => 1 << LayerMask.NameToLayer("Player");

    protected IEnumerator movementBehaviour;
    protected abstract IEnumerator Move();

    protected override void Awake()
    {
        base.Awake();

        ownerShip = FindObjectOfType<Enemy>();
        playerShip = FindObjectOfType<Player>();
    }

    public override void Fire()
    {
        if (movementBehaviour != null)
        {
            StopCoroutine(movementBehaviour);
        }

        movementBehaviour = Move();
        StartCoroutine(movementBehaviour);
    }

    protected override void Update()
    {
        base.Update();
        CheckCollisionWith<Player>();
    }

    public override void Destroy()
    {
        if (movementBehaviour != null)
        {
            StopCoroutine(movementBehaviour);
        }

        base.Destroy();
        EnemyBulletPool.Instance.ReturnToPool(this);
    }
}