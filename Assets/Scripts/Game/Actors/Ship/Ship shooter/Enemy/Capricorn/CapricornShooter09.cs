using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornShooter09 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 20;
    const float WaveSpacing = 8f;
    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2f);

        float r = PlayerPosition.GetRotationDifference(transform.position);

        for (int i = 0; i < WaveCount; i++)
        {
            float z = ((i - ((WaveCount - 1) / 2f)) * WaveSpacing) + r;
            Vector3 pos = Vector3.zero;

            SpawnProjectile(0, z, pos).Fire();

            yield return WaitForSeconds(ShootingCooldown);
        }

    }
}