public abstract class Collectible : Actor
{
    Player player;

    protected override void Awake()
    {
        base.Awake();
        player = FindObjectOfType<Player>();
    }
}