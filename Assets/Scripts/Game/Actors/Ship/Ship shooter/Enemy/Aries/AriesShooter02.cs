using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesShooter02 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 3;
    const int BulletCount = 3;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedModifier = 0.6f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1f);

        for (int i = 0; i < WaveCount; i++)
        {
            float z = PlayerPosition.GetRotationDifference(transform.position);

            for (int ii = 0; ii < BulletCount; ii++)
            {
                float s = BulletBaseSpeed + (ii * BulletSpeedModifier);
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(ii / (BulletCount - 1f));

                var bullet = SpawnProjectile(2, z, pos);
                bullet.StartCoroutine(bullet.LerpSpeed(0f, s, 1f, delay: 0.5f));
                bullet.Fire();

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(1f);
        }
    }
}