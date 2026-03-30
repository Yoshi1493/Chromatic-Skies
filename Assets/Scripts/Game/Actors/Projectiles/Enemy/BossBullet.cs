using UnityEngine;

public abstract class BossBullet : Bullet
{
    protected override int CollisionMask => 1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Special bullet");
    protected Boss parentShip;

    protected override void Awake()
    {
        base.Awake();

        parentShip = FindAnyObjectByType<Boss>();
        parentShip.LoseLifeAction += OnBossLoseLife;
        playerShip.LoseLifeAction += Destroy;
    }

    void OnBossLoseLife()
    {
        if (transform.position.IsWithinCameraBounds())
        {
            var collectible = SpawnScoreCollectible();
            collectible.foundPlayer = true;
        }

        Destroy(gameObject);
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
                SpawnGrazeParticles(pos, rot);

                hasGrazed = true;
            }
        }
        if (coll.TryGetComponent(out SpecialBullet specialBullet))
        {
            if (specialBullet is SpecialBlue0 blue)
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
    
    protected virtual Collectible SpawnScoreCollectible()
    {
        var collectible = CollectibleObjectPool.Instance.Get((int)CollectibleType.Score);

        collectible.transform.position = transform.position;
        collectible.gameObject.SetActive(true);
        collectible.enabled = true;

        return collectible;
    }

    //returns to object pool queue as disabled object
    //only called when gameobject reaches max lifetime
    //gameobject is completely destroyed (i.e. removed from scene) upon Boss losing life
    public override void Destroy()
    {
        if (movementBehaviour != null)
        {
            StopCoroutine(movementBehaviour);
        }

        MoveSpeed = 0f;
        ReturnToObjectPool();
    }

    void OnDestroy()
    {
        parentShip.LoseLifeAction -= OnBossLoseLife;
    }

    public override void ReturnToObjectPool()
    {
        BossBulletPool.Instance.ReturnToPool(this);
    }
}