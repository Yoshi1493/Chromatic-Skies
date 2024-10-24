using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class SagittariusBulletSystem31 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 10;
    const float WaveSpacing = BranchSpacing / WaveCount;
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 7;
    const float BulletSpacing = 10f;
    const float BulletBaseSpeed = 2.4f;
    const float BulletSpeedModifier = 0.2f;

    protected override float ShootingCooldown => 0.08f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float z = (i * WaveSpacing) + (ii * BranchSpacing) + ((iii - ((BulletCount - 1) / 2f)) * BulletSpacing);
                    float s = BulletBaseSpeed + (iii * BulletSpeedModifier);
                    Vector3 pos = Vector3.zero;

                    bulletData.colour = bulletData.gradient.Evaluate(iii / (BulletCount - 1f));

                    var bullet = SpawnProjectile(1, z, pos);
                    bullet.MoveSpeed = s;
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}