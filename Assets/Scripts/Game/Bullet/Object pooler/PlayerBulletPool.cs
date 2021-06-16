public class PlayerBulletPool : GenericBulletPool<PlayerBullet>
{
    ShipObject playerShipData;

    protected override void Awake()
    {
        playerShipData = FindObjectOfType<Player>().shipData;
        playerShipData.bullets.ForEach(i => objectPrefabs.Add(i.GetComponent<PlayerBullet>()));

        base.Awake();
    }
}