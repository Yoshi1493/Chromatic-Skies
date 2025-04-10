public abstract class BossBullet : EnemyBullet
{
    protected Boss ownerShip;

    protected override void Awake()
    {
        base.Awake();
        ownerShip = FindObjectOfType<Boss>();
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