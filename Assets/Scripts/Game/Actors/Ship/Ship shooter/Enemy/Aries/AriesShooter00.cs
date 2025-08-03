using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class AriesShooter00 : CommonEnemyShooter<EnemyBullet>
{
    const int RepeatCount = 4;
    const int WaveCount = 3;
    const int BranchCount = 5;
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
                float r = RandomAngleDeg;

                for (int iii = 0; iii < BranchCount; iii++)
                {
                    float z = (iii * BranchSpacing) + r;
                    float s = BulletBaseSpeed + (ii * BulletSpeedModifier);
                    Vector3 pos = Vector3.zero;

                    bulletData.colour = bulletData.gradient.Evaluate(ii / (WaveCount - 1f));

                    var bullet = SpawnProjectile(0, z, pos);
                    bullet.MoveSpeed = s;
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(0.8f);
        }
    }
}