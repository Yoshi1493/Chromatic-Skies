using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerShooter01 : CommonEnemyShooter<EnemyBullet>
{
    const int RepeatCount = 6;
    const int WaveCount = 11;
    const int BulletCount = 3;
    const float BulletBaseSpeed = 3f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1.5f);

        for (int i = 0; i < RepeatCount; i++)
        {
            float r = PlayerPosition.GetRotationDifference(transform.position);

            for (int ii = 0; ii < WaveCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float z = r;
                    float s = BulletBaseSpeed + Random.value;
                    Vector3 pos = Vector3.zero;

                    bulletData.colour = bulletData.gradient.Evaluate(s - BulletBaseSpeed);

                    var bullet = SpawnProjectile(1, z, pos);
                    bullet.StartCoroutine(bullet.LerpSpeed(0f, s, 1f, delay: 1f));
                    bullet.Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(0.5f);
        }
    }
}