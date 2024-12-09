using UnityEngine;

public abstract class EnemyBullet : Bullet
{
    protected Enemy ownerShip;

    protected override int CollisionMask => 1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Special bullet");

    protected override void Awake()
    {
        base.Awake();
        ownerShip = FindObjectOfType<Enemy>();
    }

    protected override void Update()
    {
        base.Update();

        CheckCollisionWith<Player>();
        CheckCollisionWith<PlayerGraze>();
        CheckCollisionWith<SpecialBullet>();
    }

    protected override void HandleCollision(Collider2D coll)
    {
        base.HandleCollision(coll);

        Vector3 pos = coll.ClosestPoint(transform.position);
        float rot = coll.transform.position.GetRotationDifference(transform.position);

        if (coll.TryGetComponent(out PlayerGraze playerGraze))
        {
            if (!hasGrazed)
            {
                playerGraze.GrazePlayer();
                SpawnDestructionParticles(pos, rot);

                hasGrazed = true;
            }
        }
        if (coll.TryGetComponent(out SpecialBullet specialBullet))
        {
            if (specialBullet is SpecialBlue1)
            {
                //temp hardcoded value
                playerShip.TakeDamage(-75);
            }
            Destroy();
        }
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