using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerShooter07 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 360;
    const int BulletCount = 6;
    readonly float BulletSpacing = (1f + Mathf.Sqrt(5f)) * 180f;

    protected override float ShootingCooldown => 1f / 30;

    protected override IEnumerator Shoot()
    {
        for (int i = 1; enabled; i *= -1)
        {
            yield return WaitForSeconds(2f);

            for (int ii = 0; ii < WaveCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float z = i * ii * BulletSpacing;
                    Vector3 pos = Vector3.zero;

                    SpawnProjectile(7, z, pos).Fire();
                    ii++;
                }

                yield return WaitForSeconds(ShootingCooldown);
            }
        }
    }
}