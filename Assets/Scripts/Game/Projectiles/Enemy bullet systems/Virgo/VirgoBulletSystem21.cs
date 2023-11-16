using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem21 : EnemyShooter<EnemyBullet>
{
    const float WaveSpacing = 12f;
    const int BranchCount = 15;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;
    const float BulletRotationSpeed = 90f;

    protected override float ShootingCooldown => 0.6f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; enabled; i++)
        {
            yield return WaitForSeconds(ShootingCooldown);

            for (int ii = 0; ii < BranchCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float z = (i * WaveSpacing) + (ii * BranchSpacing);
                    Vector3 pos = Vector3.zero;

                    bulletData.colour = bulletData.gradient.Evaluate(iii);

                    var bullet = SpawnProjectile(1, z, pos);
                    bullet.StartCoroutine(bullet.RotateBy((iii % 2 * 2 - 1) * BulletRotationSpeed, 6f));
                    bullet.Fire();
                }
            }
        }
    }

}