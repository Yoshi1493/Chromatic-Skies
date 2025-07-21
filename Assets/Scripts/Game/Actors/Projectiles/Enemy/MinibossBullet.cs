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
        //spawn score collectible if within camera bounds
        if (transform.position.x > -ScreenHalfWidth
            && transform.position.x < ScreenHalfWidth
            && transform.position.y > -ScreenHalfHeight
            && transform.position.y < ScreenHalfHeight)
        {
            var collectible = CollectibleObjectPool.Instance.Get((int)CollectibleType.Score);

            collectible.transform.position = transform.position;
            collectible.gameObject.SetActive(true);
            collectible.enabled = true;
        }

        Destroy(gameObject);
    }

    void OnDestroy()
    {
        parentShip.LoseLifeAction -= OnMinibossDie;
    }
}