using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem42 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 54;
    const float WaveSpacing = 10f;
    const int BranchCount = 4;
    const float BulletBaseSpeed = 4f;
    const float BulletSpeedModifier = 0.04f;
    const float BulletRotationDuration = 1f;
    const float RotationDurationModifier = 0.02f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        float r = PlayerPosition.GetRotationDifference(transform.position);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = (ii % 2 * 2 - 1) * (i * WaveSpacing) + r;
                float s = BulletBaseSpeed + (i * BulletSpeedModifier);
                float d = BulletRotationDuration + (i * RotationDurationModifier);
                Vector3 pos = Vector3.zero;

                var bullet = SpawnProjectile(2, z, pos);
                bullet.StartCoroutine(bullet.LerpSpeed(s, 2f, d));
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}