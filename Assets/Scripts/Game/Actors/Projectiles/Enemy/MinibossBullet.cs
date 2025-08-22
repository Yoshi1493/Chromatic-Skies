public abstract class MinibossBullet : EnemyBullet
{
    Miniboss parentShip;

    protected override void Awake()
    {
        base.Awake();

        parentShip = FindObjectOfType<Miniboss>();
        parentShip.DeathAction += OnMinibossDie;
        playerShip.LoseLifeAction += Destroy;
    }

    void OnMinibossDie()
    {
        if (this.IsWithinCameraBounds())
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