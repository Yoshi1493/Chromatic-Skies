public class ScoreCollectible : Collectible
{
    //increase Player score
    protected override void Collect()
    {
        
    }

    protected override void Destroy()
    {
        base.Destroy();
        CollectibleObjectPool.Instance.ReturnToPool(this, (int)CollectibleType.Score);
    }
}