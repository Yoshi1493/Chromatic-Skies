using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem51 : BossShooter<BossBullet>
{
    protected override float ShootingCooldown => 2f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(ShootingCooldown);

        while (enabled)
        {
            float x = PlayerPosition.x;
            float y = screenHalfHeight + 1f;
            float z = 0f;
            Vector3 pos = new(x, y);

            SpawnProjectile(2, z, pos, false).Fire();

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}