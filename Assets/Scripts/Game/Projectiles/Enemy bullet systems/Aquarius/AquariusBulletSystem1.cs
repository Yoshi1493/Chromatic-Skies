using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusBulletSystem1 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 18;
    const float WaveSpacing = 12f;
    const int BulletCount = 4;
    const float BulletSpacing = 15f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            float r = Random.Range(180f, 240f);

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BulletCount; ii++)
                {
                    float z = (i * WaveSpacing) - (ii * BulletSpacing) + r;
                    float s = (ii * -0.2f) + 3f;

                    var bullet = SpawnProjectile(0, z, Vector3.zero);
                    bullet.GetComponent<EnemyBullet>().MoveSpeed = s;
                    bullet.Fire();

                    bullet = SpawnProjectile(1, -z, Vector3.zero);
                    bullet.GetComponent<EnemyBullet>().MoveSpeed = s;
                    bullet.Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            SetSubsystemEnabled(1);
            yield return ownerShip.MoveToRandomPosition(2f, delay: 3f);
        }
    }
}