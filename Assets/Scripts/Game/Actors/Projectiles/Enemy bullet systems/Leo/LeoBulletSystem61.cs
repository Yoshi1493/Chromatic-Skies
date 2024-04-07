using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem61 : EnemyShooter<EnemyBullet>
{
    const float WaveSpacing = 8f;
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 8;
    const float BulletSpacing = 5f;
    const float BulletBaseSpeed = 1f;
    const float BulletSpeedModifier = 0.2f;
    const float BulletRotationSpeed = 24f;
    const float BulletRotationSpeedModifier = 6f;
    const float BulletRotationDuration = 4f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; enabled; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float z = (i * WaveSpacing) + (ii * BranchSpacing) + (iii *BulletSpacing) + 90f;
                    float r = BulletRotationSpeed + (iii * BulletRotationSpeedModifier);
                    float s = BulletBaseSpeed + (iii * BulletSpeedModifier);
                    Vector3 pos = Vector3.zero;

                    bulletData.colour = bulletData.gradient.Evaluate(iii / (BulletCount - 1f));

                    var bullet = SpawnProjectile(1, z, pos);
                    bullet.MoveSpeed = s;
                    bullet.StartCoroutine(bullet.RotateBy(r, BulletRotationDuration, false));
                    bullet.Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }
        }
    }
}