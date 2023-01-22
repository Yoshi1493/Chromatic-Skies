using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusBulletSystem1 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 18;
    const float WaveSpacing = 12f;
    const int BulletCount = 4;
    const float BulletSpacing = 15f;
    const float MinAngle = 180f;
    const float MaxAngle = 240f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            float r = Random.Range(MinAngle, MaxAngle);

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BulletCount; ii++)
                {
                    float z = (i * WaveSpacing) - (ii * BulletSpacing) + 180f;
                    float s = (ii * -0.2f) + 3f;
                    Vector3 pos = Vector3.zero;

                    var bullet = SpawnProjectile(0, z, pos);
                    bullet.GetComponent<EnemyBullet>().MoveSpeed = s;
                    bullet.Fire();

                    bullet = SpawnProjectile(1, -z, pos);
                    bullet.GetComponent<EnemyBullet>().MoveSpeed = s;
                    bullet.Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            SetSubsystemEnabled(1);

            StartMoveAction?.Invoke();
            yield return WaitForSeconds(5f);
        }
    }
}