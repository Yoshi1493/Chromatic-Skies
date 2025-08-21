using static CameraBoundaries;

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
        if (transform.position.x > -ScreenHalfWidth
            && transform.position.x < ScreenHalfWidth
            && transform.position.y > -ScreenHalfHeight
            && transform.position.y < ScreenHalfHeight)
        {
            var collectible = SpawnCollectible();
            collectible.foundPlayer = true;
        }
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
        BossBulletPool.Instance.ReturnToPool(this);
    }

    void OnDestroy()
    {
        parentShip.LoseLifeAction -= OnBossLoseLife;
    }
}