using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class ScorpioBulletSystem5 : EnemyShooter<EnemyBullet>
{
    const int BranchCount = 2;
    const int BulletCount = 4;
    const float BulletSpacing = 1.2f;
    const float BulletRotationSpeed = 90f;
    const float BulletRotationDuration = 1f;
    const float BulletRotationDurationModifier = 1f;

    protected override float ShootingCooldown => 15f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            for (int i = 0; i < BranchCount; i++)
            {
                int d = i % 2 * 2 - 1;

                for (int ii = 0; ii < BulletCount; ii++)
                {
                    float x = d * (1f + (ii * BulletSpacing));
                    float y = 1.1f * screenHalfHeight;
                    Vector3 pos = new(x, y);
                    float z = 0f;

                    var bullet = SpawnProjectile(0, z, pos, false);
                    bullet.StartCoroutine(bullet.RotateBy(d * BulletRotationSpeed, BulletRotationDuration, delay: 1f));
                    bullet.Fire();
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}