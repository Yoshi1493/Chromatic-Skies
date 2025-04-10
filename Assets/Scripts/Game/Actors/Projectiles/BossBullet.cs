public abstract class BossBullet : EnemyBullet
{
    protected Boss parentShip;

    protected override void Awake()
    {
        base.Awake();
        parentShip = FindObjectOfType<Boss>();
    }

    public override void Destroy()
    {
        if (movementBehaviour != null)
        {
            StopCoroutine(movementBehaviour);
        }

        MoveSpeed = 0f;
        BossBulletPool.Instance.ReturnToPool(this);
    }
}