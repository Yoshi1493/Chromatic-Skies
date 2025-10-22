using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerShooter01 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 8;
    const int BranchCount = 5;
    const float BranchSpacing = 15f;
    const float BulletBaseSpeed = 9f;
    const float BulletSpeedModifier = -0.9f;
    const float BulletRotationSpeed = 30f;
    const float BulletRotationSpeedModifier = 10f;
    const float BulletRotationDuration = 3f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1.5f);

        float t = PlayerPosition.GetRotationDifference(transform.position);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = ((ii - ((BranchCount - 1) / 2f)) * BranchSpacing) + t;
                float r = (ii - ((BranchCount - 1) / 2f)) * (BulletRotationSpeed + (i * BulletRotationSpeedModifier));
                float s = BulletBaseSpeed + (i * BulletSpeedModifier);
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

                var bullet = SpawnProjectile(1, z, pos);
                bullet.MoveSpeed = s;
                bullet.StartCoroutine(bullet.RotateBy(r, BulletRotationDuration, delay: 1.5f));
                bullet.Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}