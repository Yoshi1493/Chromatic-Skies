using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesShooter04 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 6;
    const int BulletCount = 6;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletRotationAmount = 60f;
    const float BulletRotationDuration = 2f;

    protected override float ShootingCooldown => 0.2f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            yield return WaitForSeconds(ShootingCooldown);

            int d = i % 2;
            float r = Random.Range(0f, BulletSpacing);

            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = (ii * BulletSpacing) + r;
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(d);

                var bullet = SpawnProjectile(4, z, pos);
                bullet.StartCoroutine(bullet.RotateBy(d * BulletRotationAmount, BulletRotationDuration, delay: 1f));
                bullet.Fire();
            }
        }
    }
}