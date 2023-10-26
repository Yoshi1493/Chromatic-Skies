using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusBulletSystem5 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 5;
    const float BranchSpacing = 1f;

    protected override float ShootingCooldown => base.ShootingCooldown;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        float y = screenHalfHeight * 1.1f;
        float z = 0f;

        for (int i = 0; enabled; i++)
        {
            float x = 0.8f * Random.Range(-screenHalfWidth, screenHalfWidth);
            Vector3 pos = new(x, y);

            bulletData.colour = bulletData.gradient.Evaluate(Mathf.InverseLerp(screenHalfWidth, screenHalfWidth, x));
            SpawnProjectile(0, z, pos, false).Fire();


            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}