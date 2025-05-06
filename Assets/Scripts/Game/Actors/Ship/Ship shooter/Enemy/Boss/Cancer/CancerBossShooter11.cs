using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerBossShooter11 : BossShooter<BossBullet>
{
    const float ArcHalfWidth = 45f;
    const int WaveCount = 60;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            float z = Random.Range(-ArcHalfWidth, ArcHalfWidth) + 180f;
            Vector3 pos = Vector3.zero;

            SpawnProjectile(2, z, pos).Fire();

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}