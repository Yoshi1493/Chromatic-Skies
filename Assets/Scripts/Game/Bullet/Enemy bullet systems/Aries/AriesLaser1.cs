public class AriesLaser1 : Laser
{
    Player player;

    protected override void Awake()
    {
        base.Awake();
        player = FindObjectOfType<Player>();
    }

    void Start()
    {
        this.LookAt(player, 1);
    }
}