using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem31 : EnemyShooter<EnemyBullet>
{
    const float WaveSpacing = 10f;
    const int BranchCount = 3;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 3;
    const float BulletSpacing = 8f;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedModifier = 0.5f;

    protected override float ShootingCooldown => 0.2f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; enabled; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float z = (i * WaveSpacing) + (ii * BranchSpacing) + (iii * BulletSpacing);
                    float s = BulletBaseSpeed + (iii * BulletSpeedModifier);
                    Vector3 pos = Vector3.zero;

                    bulletData.colour = bulletData.gradient.Evaluate(iii / (BulletCount - 1f));
                    var bullet = SpawnProjectile(1, z, pos);
                    bullet.MoveSpeed = s;
                    bullet.Fire();
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}