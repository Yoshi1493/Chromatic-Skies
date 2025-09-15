using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoShooter04 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 9;
    const float BulletSpacing = 12f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2f);

        float r = PlayerPosition.GetRotationDifference(transform.position);

        for (int i = 0; i < WaveCount; i++)
        {
            int bulletCount = (int)Mathf.PingPong(i, WaveCount / 2) + 1;

            for (int ii = 0; ii < bulletCount; ii++)
            {
                float z = ((ii - (bulletCount - 1) / 2f) * BulletSpacing) + r;
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

                SpawnProjectile(4, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}