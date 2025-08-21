public class HealthCollectible : Collectible
{
    public override void Destroy()
    {
        base.Destroy();
        CollectibleObjectPool.Instance.ReturnToPool(this, (int)CollectibleType.Health);
    }
}