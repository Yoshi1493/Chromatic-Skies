using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusShooter1 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 3;
    const int BulletCount = 3;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2f);

        for (int i = 0; i < WaveCount; i++)
        {
            float z = PlayerPosition.GetRotationDifference(transform.position);
            Vector3 pos = Vector3.zero;

            for (int ii = 0; ii < BulletCount; ii++)
            {
                SpawnProjectile(0, z, pos).Fire();
            }
        }
    }
}