using UnityEngine;

public abstract class EnemyBullet : Bullet
{
    protected override int CollisionMask => 1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Special bullet");

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
                SpawnGrazeParticles(pos, rot);

                hasGrazed = true;
            }
        }
        if (coll.TryGetComponent(out SpecialBullet specialBullet))
        {
            if (specialBullet is SpecialBlue1 blue)
            {
                int healAmount = DamageCalculator.CalculateHealing(playerShip.shipData.MaxHealth.Value, blue.HitCount);
                playerShip.TakeDamage(healAmount);

                blue.RegisterHit();
            }

            if (projectileData.destructible)
            {
                Destroy();
            }
        }
    }

    protected virtual Collectible SpawnCollectible()
    {
        var collectible = CollectibleObjectPool.Instance.Get((int)CollectibleType.Score);

        collectible.transform.position = transform.position;
        collectible.gameObject.SetActive(true);
        collectible.enabled = true;

        Destroy(gameObject);

        return collectible;
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