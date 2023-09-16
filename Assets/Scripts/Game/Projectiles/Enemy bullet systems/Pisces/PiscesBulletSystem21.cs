using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class PiscesBulletSystem21 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 15;
    const float WaveOffset = 4f;
    const float SafeZone = 20f;
    const int BranchCount = ((int)(360 - (SafeZone * 2)) / (int)BranchSpacing / 2) + 1;
    const float BranchSpacing = 10f;
    const float BulletBaseSpeed = 3f;
    const float BulletSpeedMultiplier = 0.1f;

    protected override float ShootingCooldown => 0.2f;

    protected override IEnumerator Shoot()
    {
        float r = PositiveOrNegativeOne;

        for (int i = 0; i < WaveCount; i++)
        {
            float t = Mathf.PingPong(i, WaveCount / 4) * WaveOffset;
            float s = BulletBaseSpeed - (i * BulletSpeedMultiplier);

            bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = (SafeZone * 0.5f) + (t * r) + (ii * BranchSpacing);
                Vector3 pos = Vector3.zero;

                var bullet = SpawnProjectile(1, z, pos);
                bullet.MoveSpeed = s;
                bullet.Fire();

                bullet = SpawnProjectile(1, z + 180f, pos);
                bullet.MoveSpeed = s;
                bullet.Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}