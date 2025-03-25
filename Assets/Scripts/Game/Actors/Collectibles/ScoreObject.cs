public class ScoreObject : Collectible
{
    public override void Destroy()
    {
        base.Destroy();
        CollectibleObjectPool.Instance.ReturnToPool(this, (int)CollectibleType.Score);
    }
}