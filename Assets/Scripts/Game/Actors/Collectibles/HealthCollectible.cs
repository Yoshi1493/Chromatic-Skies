public class HealthCollectible : Collectible
{
    //restore Player health
    protected override void Collect()
    {
        
    }

    protected override void Destroy()
    {
        base.Destroy();
        CollectibleObjectPool.Instance.ReturnToPool(this, (int)CollectibleType.Health);
    }
}