public abstract class BossLaser : Laser
{
    protected Boss parentShip;

    protected override void Awake()
    {
        base.Awake();

        parentShip = FindObjectOfType<Boss>();
        parentShip.LoseLifeAction += OnBossLoseLife;
    }
    void OnBossLoseLife()
    {
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        parentShip.LoseLifeAction -= OnBossLoseLife;
    }

    public override void ReturnToObjectPool()
    {
        BossLaserPool.Instance.ReturnToPool(this);
    }
}