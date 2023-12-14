using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem51 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 4;
    const int BranchCount = 5;
    const float BranchSpacing = 18f;
    const int BulletCount = 5;
    const float BulletSpacing = 3f;
    const float BulletBaseSpeed = 2.25f;
    const float BulletSpeedModifier = 0.75f;

    protected override float ShootingCooldown => 1f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            float r = PlayerPosition.GetRotationDifference(transform.position);

            for (int ii = 0; ii < BranchCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float z = (ii - ((BranchCount - 1) / 2f)) * BranchSpacing + r;
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

       enabled = false;
    }
}