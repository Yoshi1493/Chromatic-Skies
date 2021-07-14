using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem1 : EnemyBulletSystem
{
    protected override IEnumerator Start()
    {
        EnemyBulletPool.Instance.UpdatePoolableBullets(enemyBullets);
        yield return WaitForSeconds(3f);

        float goldenRatio = (1 + Mathf.Sqrt(5)) * 180;

        while (enabled)
        {
            spawnPositions[0].Rotate(goldenRatio * Vector3.forward);
            SpawnBullet(0, 0);

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}