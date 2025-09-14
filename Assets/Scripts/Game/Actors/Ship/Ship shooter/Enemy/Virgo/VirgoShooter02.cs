using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoShooter02 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 9;
    const float WaveSpacing = 10f;
    const int BulletCount = 2;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedModifier = 0.1f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1.5f);

        float r = PlayerPosition.GetRotationDifference(transform.position);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = ((i - ((WaveCount - 1) / 2f)) * WaveSpacing) + r;
                float s = (ii % 2 * WaveCount * BulletSpeedModifier) + BulletBaseSpeed + (-(ii % 2 * 2 - 1) * (i * BulletSpeedModifier));
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

                var bullet = SpawnProjectile(2, z, pos);
                bullet.StartCoroutine(bullet.LerpSpeed(0f, s, 1f, delay: 0.5f));
                bullet.Fire();
            }
        }
    }
}