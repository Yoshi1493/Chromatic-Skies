using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem1 : EnemyBulletSystem
{
    protected override IEnumerator Shoot()
    {
        float goldenRatio = (1 + Mathf.Sqrt(5)) * 180;

        while (enabled)
        {
            yield return base.Shoot();

            for (int i = 0; i < 144; i++)
            {
                float z = i * goldenRatio;
                SpawnBullet(0, z, transform.up.RotateVectorBy(z)).Fire();

                yield return WaitForSeconds(ShootingCooldown);
            }
        }
    }
}