using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBulletSystem51 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 20;
    const int BranchCount = 15;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedModifier = 0.2f;
    const float BulletRotationSpeed = -30f;
    const float BulletRotationSpeedModifier = -2f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        float r = PlayerPosition.GetRotationDifference(transform.position);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = (ii * BranchSpacing) + r;
                float s = BulletBaseSpeed + (i * BulletSpeedModifier);
                float t = BulletRotationSpeed + (i * BulletRotationSpeedModifier);
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

                var bullet = SpawnProjectile(1, z, pos);
                bullet.MoveSpeed = s;
                bullet.StartCoroutine(bullet.RotateBy(t, 2f));
                bullet.Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}