using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class GeminiBulletSystem11 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 12;
    const int BranchCount = 4;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 5;
    const float BulletSpacing = 5f;
    public const float BulletBaseSpeed = 2.4f;
    const float BulletSpeedModifier = 0.1f;

    protected override float ShootingCooldown => 0.3f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float r = RandomAngleDeg;
                Vector3 pos = transform.up.RotateVectorBy(RandomAngleDeg);

                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float t = Mathf.PingPong(iii, BulletCount / 2);
                    float z = (ii * BranchSpacing) + (iii * BulletSpacing) + r;
                    float s = BulletBaseSpeed + (t * BulletSpeedModifier);

                    bulletData.colour = bulletData.gradient.Evaluate(Mathf.InverseLerp(0f, BulletCount / 2, t)); 

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