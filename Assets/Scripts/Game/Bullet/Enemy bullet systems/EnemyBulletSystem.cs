using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletSystem : Shooter
{
    [SerializeField] protected List<EnemyBullet> enemyBullets = new List<EnemyBullet>();

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
}