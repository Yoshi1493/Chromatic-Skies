using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class CapricornShooter04 : CommonEnemyShooter<EnemyBullet>
{
    const int RepeatCount = 2;
    const int WaveCount = 100;
    const float WaveSpacing = 9f;
    const int BranchCount = 2;
    const float BulletSpawnRadius = 0.5f;
    const float BulletSpawnRadiusModifier = 0.02f;
    const float BulletBaseSpeed = 2.5f;
    const float BulletSpeedModifier = 0.01f;

    protected override float ShootingCooldown => 1f / 60;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2f);

        float r = PlayerPosition.GetRotationDifference(transform.position);

        for (int i = 0; i < RepeatCount; i++)
        {
            for (int ii = 0; ii < WaveCount; ii++)
            {
                for (int iii = 0; iii < BranchCount; iii++)
                {
                    float z = RandomAngleDeg;
                    float s = BulletBaseSpeed + (ii * BulletSpeedModifier);
                    float t = (iii % 2 * 2 - 1) * (ii * WaveSpacing + r);
                    Vector3 pos = (BulletSpawnRadius + (ii * BulletSpawnRadiusModifier)) * transform.up.RotateVectorBy(t);

                    bulletData.colour = bulletData.gradient.Evaluate(ii / (WaveCount - 1f));

                    var bullet = SpawnProjectile(4, z, pos);
                    bullet.MoveSpeed = s;
                    bullet.Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(3f);
        }
    }
}