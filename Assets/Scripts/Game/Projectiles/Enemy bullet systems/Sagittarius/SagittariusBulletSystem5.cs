using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class SagittariusBulletSystem5 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 18;
    const int BulletCount = 12;
    const float BulletBaseSpeed = 0.5f;
    const float BulletSpeedModifier = 1.25f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            StartMoveAction?.Invoke();

            for (int i = 0; i < WaveCount; i++)
            {
                yield return WaitForSeconds(ShootingCooldown);

                for (int ii = 0; ii < BulletCount; ii++)
                {
                    float z = 0f;
                    float s = BulletBaseSpeed + (ii * BulletSpeedModifier);
                    Vector3 pos = Vector3.zero;

                    var bullet = SpawnProjectile(0, z, pos);
                    bullet.StartCoroutine(bullet.LerpSpeed(s, 0, 1f, delay: 0.5f));
                }
            }

            yield return WaitForSeconds(10f);
        }

    }
}