using System.Collections;
using UnityEngine;

public abstract class EnemyBullet : Bullet
{
    protected Enemy ownerShip;
    protected Player playerShip;

    void Start()
    {
        ownerShip = FindObjectOfType<Enemy>();
        playerShip = FindObjectOfType<Player>();
    }

    protected override void Update()
    {
        base.Update();
        CheckCollisionWith<Player>();
    }

    public override void Destroy()
    {
        if (movementBehaviour != null) StopCoroutine(movementBehaviour);
        EnemyBulletPool.Instance.ReturnToPool(this);
    }

    public void Fire()
    {
        if (movementBehaviour != null)
            StopCoroutine(movementBehaviour);

        movementBehaviour = Move();
        StartCoroutine(movementBehaviour);
    }

    protected abstract IEnumerator Move();
}