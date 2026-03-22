public class PowerCollectible : Collectible
{
    //increase Player attack power
    protected override void Collect()
    {
        
    }

    protected override void Destroy()
    {
        base.Destroy();
        CollectibleObjectPool.Instance.ReturnToPool(this, (int)CollectibleType.Power);
    }
}