public class PSInvinciblePlayerShield : ParticleEffect
{
    Player playerShip;

    protected override void Awake()
    {
        base.Awake();
        playerShip = FindAnyObjectByType<Player>();
    }

    protected override void ReturnToPool()
    {
        VFXObjectPool.Instance.ReturnToPool(this, (int)VFXType.InvinciblePlayerShield);
    }

    protected override void Update()
    {
        base.Update();
        transform.position = playerShip.transform.position;
    }
}