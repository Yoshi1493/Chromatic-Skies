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

            for (int i = 0; i < 9999; i++)
            {
                float z = i * goldenRatio;
                SpawnBullet(1, z, Vector2.zero).Fire();

                yield return WaitForSeconds(ShootingCooldown / 8f);
            }
        }
    }
}