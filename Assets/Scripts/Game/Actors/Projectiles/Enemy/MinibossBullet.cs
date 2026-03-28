public abstract class MinibossBullet : EnemyBullet
{
    Miniboss parentShip;

    protected override void Awake()
    {
        base.Awake();

        parentShip = FindAnyObjectByType<Miniboss>();
        parentShip.DeathAction += OnMinibossDie;
        playerShip.LoseLifeAction += Destroy;
    }

    void OnMinibossDie()
    {
        if (transform.position.IsWithinCameraBounds())
        {
            SpawnScoreCollectible();
        }

        Destroy(gameObject);
    }

    void OnDestroy()
    {
        parentShip.LoseLifeAction -= OnMinibossDie;
    }
}