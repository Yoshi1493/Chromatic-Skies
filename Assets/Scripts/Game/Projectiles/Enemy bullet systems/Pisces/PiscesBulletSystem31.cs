using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem31 : EnemyShooter<EnemyBullet>
{
    const float WaveSpacing = 8f;
    const int BranchCount = 3;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 3;
    const float BulletSpacing = 6f;
    const float BulletSpeedMultiplier = 0.5f;

    protected override IEnumerator Shoot()
    {
        int i = 0;

        while (enabled)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float z = (i * WaveSpacing) + (ii * BranchSpacing) + (iii * BulletSpacing);
                    float s = (iii * BulletSpeedMultiplier) + 2f;
                    Vector3 pos = Vector3.zero;

                    bulletData.colour = bulletData.gradient.Evaluate(iii / (BulletCount - 1f));
                    var bullet = SpawnProjectile(1, z, pos);
                    bullet.MoveSpeed = s;
                    bullet.Fire();
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
            i = (int)Mathf.Repeat(++i, 360f / WaveSpacing);
        }
    }
}