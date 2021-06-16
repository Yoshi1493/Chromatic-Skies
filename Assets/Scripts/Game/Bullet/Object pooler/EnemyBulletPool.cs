using UnityEngine;

public class EnemyBulletPool : GenericBulletPool<EnemyBullet>
{
    [SerializeField] ShipObject enemyShipData;

    protected override void Awake()
    {
        enemyShipData.bullets.ForEach(i => objectPrefabs.Add(i.GetComponent<EnemyBullet>()));
        base.Awake();
    }
}