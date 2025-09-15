using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoShooter04 : CommonEnemyShooter<EnemyBullet>
{
    const float RepeatCount = 2;
    const int WaveCount = 9;
    const float BulletSpacing = 12f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2f);

        for (int i = 0; i < RepeatCount; i++)
        {
            float r = PlayerPosition.GetRotationDifference(transform.position);

            for (int ii = 0; ii < WaveCount; ii++)
            {
                int bulletCount = (int)Mathf.PingPong(ii, WaveCount / 2) + 1;

                for (int iii = 0; iii < bulletCount; iii++)
                {
                    float z = ((iii - (bulletCount - 1) / 2f) * BulletSpacing) + r;
                    Vector3 pos = Vector3.zero;

                    bulletData.colour = bulletData.gradient.Evaluate(ii / (WaveCount - 1f));

                    SpawnProjectile(4, z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(3f);
        }
    }
}