using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletSystem : Shooter
{
    [SerializeField] protected List<EnemyBullet> enemyBullets = new List<EnemyBullet>();

    protected override void SpawnBullet(int index)
    {
        var newBullet = EnemyBulletPool.Instance.Get(index);

        newBullet.transform.SetPositionAndRotation(spawnPositions[0].position, spawnPositions[0].rotation);
        newBullet.gameObject.SetActive(true);
    }
}