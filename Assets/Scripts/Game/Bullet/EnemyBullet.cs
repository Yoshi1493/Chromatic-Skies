using System.Collections;
using UnityEngine;

public abstract class EnemyBullet : Bullet
{
    protected Enemy ownerShip;
    protected Player playerShip;

    protected override int CollisionMask => 1 << LayerMask.NameToLayer("Player");

    protected IEnumerator movementBehaviour;
    protected abstract IEnumerator Move();

    void Start()
    {
        ownerShip = FindObjectOfType<Enemy>();
        playerShip = FindObjectOfType<Player>();
    }

    public void Fire()
    {
        if (movementBehaviour != null)
            StopCoroutine(movementBehaviour);

        movementBehaviour = Move();
        StartCoroutine(movementBehaviour);
    }

    protected override void Update()
    {
        base.Update();
        CheckCollisionWith<Player>();
    }

    protected override void HandleCollisionWithShip<TShip>(Collider2D coll)
    {
        //get particle spawn position+rotation
        Vector3 pos = coll.ClosestPoint(playerShip.transform.position);
        float rot = coll.transform.position.GetRotationDifference(transform.position);
        SpawnDestructionParticles(pos, rot);

        base.HandleCollisionWithShip<TShip>(coll);
    }

    public override void Destroy()
    {
        if (movementBehaviour != null)
            StopCoroutine(movementBehaviour);

        base.Destroy();
        EnemyBulletPool.Instance.ReturnToPool(this);
    }
}