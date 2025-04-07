using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem11 : BossShooter<BossBullet>
{
    const int WaveCount = 6;
    const int BulletCount = 6;
    const float SpawnMaxAngle = 75f;

    List<float> bulletSpawnData = new(WaveCount);

    protected override float ShootingCooldown => 0.25f;

    protected override void Awake()
    {
        base.Awake();

        for (int i = 0; i < WaveCount; i++)
        {
            float z = -SpawnMaxAngle + (i * (SpawnMaxAngle * 2) / (WaveCount - 1));
            bulletSpawnData.Add(z);
        }
    }

    protected override IEnumerator Shoot()
    {
        bulletSpawnData.Randomize();

        for (int i = 0; i < WaveCount; i++)
        {
            float z = bulletSpawnData[i];
            Vector3 pos = Vector3.zero;

            SpawnProjectile(1, z, pos).Fire();
            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}