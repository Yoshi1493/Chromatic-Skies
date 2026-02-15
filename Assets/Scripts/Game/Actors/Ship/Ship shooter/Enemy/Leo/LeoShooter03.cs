using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoShooter03 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 12;
    const float BulletBaseSpeed = 4f;
    const float BulletSpeedModiifer = -0.2f;
    const float BulletSpeedDelay = 1f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1f);

        for (int i = 0; i < WaveCount; i++)
        {
            float z = PlayerPosition.GetRotationDifference(transform.position);
            float s = BulletBaseSpeed + (i * BulletSpeedModiifer);
            Vector3 pos = Vector3.zero;

            bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

            var bullet = SpawnProjectile(0, z, pos);
            bullet.StartCoroutine(bullet.LerpSpeed(0f, s, 1f, delay: BulletSpeedDelay + (i * ShootingCooldown)));
            bullet.Fire();
        }
    }
}