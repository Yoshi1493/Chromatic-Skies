public class ScoreCollectible : Collectible
{
    public override void Destroy()
    {
        base.Destroy();
        CollectibleObjectPool.Instance.ReturnToPool(this, (int)CollectibleType.Score);
    }
}