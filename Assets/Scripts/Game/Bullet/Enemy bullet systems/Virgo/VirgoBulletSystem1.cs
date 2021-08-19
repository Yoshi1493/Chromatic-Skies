using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem1 : EnemyBulletSystem
{
    protected override IEnumerator Shoot()
    {
        EnemyBulletPool.Instance.UpdatePoolableObjects(enemyBullets);
        yield return WaitForSeconds(3f);

        float goldenRatio = (1 + Mathf.Sqrt(5)) * 180;

        int i = 0;
        while (enabled)
        {
            float z = i * goldenRatio;
            SpawnBullet(0, z, transform.up.RotateVectorBy(z));

            yield return WaitForSeconds(ShootingCooldown);
            i++;
        }
    }
}