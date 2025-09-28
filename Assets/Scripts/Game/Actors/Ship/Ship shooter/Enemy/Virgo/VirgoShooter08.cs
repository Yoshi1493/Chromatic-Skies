using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class VirgoShooter08 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 55;
    readonly float WaveSpacing = (1f + Mathf.Sqrt(5f)) * 180f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2f);

        float r = RandomAngleDeg;
        bulletData.colour = bulletData.gradient.Evaluate(Random.value);

        for (int i = 0; i < WaveCount; i++)
        {
            float z = (i * WaveSpacing) + r;
            Vector3 pos = Vector3.zero;
            SpawnProjectile(8, z, pos).Fire();

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}