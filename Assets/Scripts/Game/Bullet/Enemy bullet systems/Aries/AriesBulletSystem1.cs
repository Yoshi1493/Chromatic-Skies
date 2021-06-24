using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem1 : EnemyBulletSystem
{
    IEnumerator Start()
    {
        EnemyBulletPool.Instance.UpdatePoolableBullets(enemyBullets);

        float goldenRatio = (1 + Mathf.Sqrt(5)) * 180;        

        for (int i = 0; i < 1000; i++)
        {
            spawnPositions[0].Rotate(goldenRatio * Vector3.forward);
            SpawnBullet(0, 0);

            yield return WaitForSeconds(ShootingCooldown);
        }

        yield return WaitForSeconds(3f);
    }
}