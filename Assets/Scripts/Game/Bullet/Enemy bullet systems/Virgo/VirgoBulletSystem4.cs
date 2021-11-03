using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem4 : EnemyShooter<EnemyBullet>
{
    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        while (enabled)
        {
            yield return WaitForSeconds(1f);

            float n = 0f;
            float r = Random.value - 0.5f;
            float t = Random.value * 360;

            for (int i = 0; i < 200; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    float z = j * 72f + n + t;
                    SpawnProjectile(0, z, Vector2.zero).Fire();
                }

                n += i * Mathf.Sign(r);
                yield return WaitForSeconds(ShootingCooldown);
            }
        }
    }
}