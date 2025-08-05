using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesShooter09 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 2;
    const int BranchCount = 20;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletRotationSpeed = 30f;
    const float BulletRotationDuration = 2f;

    protected override float ShootingCooldown => 1f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            yield return WaitForSeconds(ShootingCooldown);

            float r = PlayerPosition.GetRotationDifference(transform.position);

            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = (ii * BranchSpacing) + r;
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(ii / (BranchCount - 1f));

                var bullet = SpawnProjectile(0, z, pos);
                bullet.StartCoroutine(bullet.RotateBy((i % 2 * 2 - 1) * BulletRotationSpeed, BulletRotationDuration));
                bullet.Fire();
            }
        }
    }
}