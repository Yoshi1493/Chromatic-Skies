using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerBulletSystem2 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 6;
    const float BulletSpacing = 360f / BulletCount;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            float r = PlayerPosition.GetRotationDifference(transform.position) + (BulletSpacing / 2f);

            for (int i = 0; i < BulletCount; i++)
            {
                float z = (i * BulletSpacing) + r;
                Vector3 pos = Vector3.zero;

                SpawnProjectile(0, z, pos).Fire();
            }

            yield return WaitForSeconds(8f);

            StartMoveAction?.Invoke();
            yield return WaitForSeconds(2f);
        }
    }
}