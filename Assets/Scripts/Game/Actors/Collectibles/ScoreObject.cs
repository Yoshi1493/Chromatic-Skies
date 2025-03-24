public class ScoreObject : Collectible
{
    protected override void Destroy()
    {
        base.Destroy();
        CollectibleObjectPool.Instance.ReturnToPool(this, (int)CollectibleType.Score);
    }
}