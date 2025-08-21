using static CameraBoundaries;

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
        if (transform.position.x > -ScreenHalfWidth
            && transform.position.x < ScreenHalfWidth
            && transform.position.y > -ScreenHalfHeight
            && transform.position.y < ScreenHalfHeight)
        {
            SpawnCollectible();
        }
    }

    void OnDestroy()
    {
        parentShip.LoseLifeAction -= OnMinibossDie;
    }
}