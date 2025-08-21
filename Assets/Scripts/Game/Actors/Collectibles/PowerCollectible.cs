public class PowerCollectible : Collectible
{
    public override void Destroy()
    {
        base.Destroy();
        CollectibleObjectPool.Instance.ReturnToPool(this, (int)CollectibleType.Power);
    }
}