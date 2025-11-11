public abstract class BossBullet : EnemyBullet
{
    protected Boss parentShip;

    protected override void Awake()
    {
        base.Awake();

        parentShip = FindObjectOfType<Boss>();
        parentShip.LoseLifeAction += OnBossLoseLife;
        playerShip.LoseLifeAction += Destroy;
    }

    void OnBossLoseLife()
    {
        if (this.IsWithinCameraBounds())
        {
            var collectible = SpawnScoreCollectible();
            collectible.foundPlayer = true;
        }

        Destroy(gameObject);
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