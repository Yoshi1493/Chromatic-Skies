using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusShooter01 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 5;
    const float BulletSpacing = 5f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2f);

        for (int i = 0; i < BulletCount; i++)
        {
            float r = PlayerPosition.GetRotationDifference(transform.position);
            float z = ((i - (BulletCount - 1) / 2) * BulletSpacing) + r;
            Vector3 pos = Vector3.zero;

            SpawnProjectile(0, z, pos).Fire();
        }
    }
}