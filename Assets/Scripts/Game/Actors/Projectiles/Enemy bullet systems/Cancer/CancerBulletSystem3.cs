using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerBulletSystem3 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 6;
    const float BulletSpacing = 360f / BulletCount;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            for (int i = 0; i < BulletCount; i++)
            {
                float z = (i + 0.5f) * BulletSpacing;
                Vector3 pos = Vector3.zero;

                SpawnProjectile(0, z, pos).Fire();
            }

            yield return WaitForSeconds(5f);

            StartMoveAction?.Invoke();
            SetSubsystemEnabled(1);
            yield return WaitForSeconds(2f);
        }
    }
}