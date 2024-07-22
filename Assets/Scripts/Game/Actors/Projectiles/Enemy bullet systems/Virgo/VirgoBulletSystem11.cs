using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem11 : EnemyShooter<EnemyBullet>
{
    const float WaveSpacing = 15f;
    const int BranchCount = 5;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletRotationSpeed = BranchSpacing / 2f;

    protected override float ShootingCooldown => 0.3f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1f);

        for (int i = 0; enabled; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = (i * WaveSpacing) + (ii * BranchSpacing);
                Vector3 pos = Vector3.zero;

                var bullet = SpawnProjectile(1, z, pos);
                bullet.StartCoroutine(bullet.RotateBy(BulletRotationSpeed, 3f));
                bullet.Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}