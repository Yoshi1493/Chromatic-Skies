using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem4 : EnemyShooter<EnemyBullet>
{
    const int MinGridLength = 3;
    const int MaxGridLength = 8;
    const float BulletSpacing = 0.2f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            int gridLength = Random.Range(MinGridLength, MaxGridLength);

            for (int i = 0; i < gridLength; i++)
            {
                for (int ii = 0; ii < gridLength; ii++)
                {
                    float z = 0f;
                    Vector3 pos = Vector3.zero;

                    SpawnProjectile(0, z, pos, false);
                    yield return WaitForSeconds(ShootingCooldown);
                }
            }
        }
    }
}