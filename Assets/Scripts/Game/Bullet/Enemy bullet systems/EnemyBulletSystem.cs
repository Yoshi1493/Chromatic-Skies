using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletSystem : Shooter
{
    [SerializeField] protected List<EnemyBullet> enemyBullets = new List<EnemyBullet>();
    [SerializeField] protected List<Laser> enemyLasers = new List<Laser>();

    protected override void Awake()
    {
        base.Awake();

        GetComponentInParent<Enemy>().LoseLifeAction += DestroyAllBullets<EnemyBullet>;
    }

    protected override void SpawnBullet(int bulletIndex, int spawnPositionIndex)
    {
        var newBullet = EnemyBulletPool.Instance.Get(bulletIndex);

        newBullet.transform.SetPositionAndRotation(spawnPositions[spawnPositionIndex].position, spawnPositions[spawnPositionIndex].rotation);
        newBullet.gameObject.SetActive(true);
    }

    protected void SpawnLaser()
    {
        var newLaser = EnemyLaserPool.Instance.Get(0);

        newLaser.transform.SetPositionAndRotation(spawnPositions[0].position, spawnPositions[0].rotation);
        newLaser.gameObject.SetActive(true);
    }
}