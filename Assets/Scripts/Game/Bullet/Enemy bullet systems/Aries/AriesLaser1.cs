public class AriesLaser1 : Laser
{
    Player player;

    protected override void Awake()
    {
        base.Awake();
        player = FindObjectOfType<Player>();
    }

    protected override void Update()
    {
        base.Update();
    }
}