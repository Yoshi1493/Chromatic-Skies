using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class CapricornShooter09 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 2;
    const int BulletCount = 30;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletBaseSpeed = -4f;
    const float BulletSpeedModifier = 6.5f;
    const float BulletRotationSpeed = 30f;
    const float BulletRotationDuration = 2f;

    //protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1.5f);

        float r = PlayerPosition.GetRotationDifference(transform.position);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = (ii * BulletSpacing) + r;
                float s = BulletBaseSpeed + (i * BulletSpeedModifier);
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(i);

                var bullet = SpawnProjectile(9, z, pos);
                bullet.StartCoroutine(bullet.RotateBy(i * Random.Range(BulletRotationSpeed, BulletRotationSpeed * 2f) * PositiveOrNegativeOne, BulletRotationDuration, delay: 0.5f));
                bullet.StartCoroutine(bullet.LerpSpeed(0f, s, 1f, delay: 0.5f));
                bullet.Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}