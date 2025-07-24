using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesShooter00 : EnemyShooter<EnemyBullet>
{
    const int RepeatCount = 3;
    const int WaveCount = 3;
    const int BranchCount = 3;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletBaseSpeed = 2.5f;
    const float BulletSpeedModifier = 0.6f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(0.8f);

        for (int i = 0; i < RepeatCount; i++)
        {
            for (int ii = 0; ii < WaveCount; ii++)
            {
                for (int iii = 0; iii < BranchCount; iii++)
                {
                    float z = BranchSpacing;
                    float s = BulletBaseSpeed + (ii * BulletSpeedModifier);
                    Vector3 pos = Vector3.zero;

                    bulletData.colour = bulletData.gradient.Evaluate(iii / (BranchCount - 1f));

                    var bullet = SpawnProjectile(0, z, pos);
                    bullet.MoveSpeed = s;
                    bullet.Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(0.8f);
        }
    }
}