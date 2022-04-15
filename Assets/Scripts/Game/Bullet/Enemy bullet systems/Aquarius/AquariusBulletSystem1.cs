using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusBulletSystem1 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 18;
    const int BulletCount = 4;
    const float WaveSpacing = 10f;
    const float BulletSpacing = 10f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                for (int j = 0; j < BulletCount; j++)
                {
                    float z = (i * WaveSpacing) - (j * BulletSpacing) + 180f;

                    var bullet = SpawnProjectile(0, z, Vector3.zero);
                    bullet.GetComponent<Bullet>().MoveSpeed = (j * -0.2f) + 3f;
                    bullet.Fire();

                    bullet = SpawnProjectile(1, -z, Vector3.zero);
                    bullet.GetComponent<Bullet>().MoveSpeed = (j * -0.2f) + 3f;
                    bullet.Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return ownerShip.MoveToRandomPosition(2f, delay: 3f);
        }
    }
}