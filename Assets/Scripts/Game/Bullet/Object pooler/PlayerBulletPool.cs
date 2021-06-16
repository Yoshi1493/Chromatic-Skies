public class PlayerBulletPool : GenericBulletPool<PlayerBullet>
{
    protected override void Awake()
    {
        shipData = FindObjectOfType<Player>().shipData;
        base.Awake();
    }
}