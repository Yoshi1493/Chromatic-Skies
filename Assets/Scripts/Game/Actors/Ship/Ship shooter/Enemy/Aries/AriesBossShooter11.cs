using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBossShooter11 : BossShooter<BossBullet>
{
    const int BulletCount = 24;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletBaseSpeed = 3.5f;
    const float BulletSpeedModifier = -0.5f;

    protected override float ShootingCooldown => 1.0f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(3f);

        while (enabled)
        {
            float r = PlayerPosition.GetRotationDifference(transform.position);

            for (int i = 0; i < BulletCount; i++)
            {
                float z = (i * BulletSpacing) + r;
                float s = BulletBaseSpeed + ((i % 2 * 2 - 1) * BulletSpeedModifier);
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(i % 2);

                var bullet = SpawnProjectile(1, z, pos);
                bullet.StartCoroutine(bullet.LerpSpeed(0f, s, 1f, delay: 0.5f));
                bullet.Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}