using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusShooter04 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 5;

    protected override float ShootingCooldown => 0.8f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            yield return WaitForSeconds(ShootingCooldown);

            float z = PlayerPosition.GetRotationDifference(transform.position);
            Vector3 pos = Vector3.zero;

            SpawnProjectile(0, z, pos).Fire();
        }
    }
}