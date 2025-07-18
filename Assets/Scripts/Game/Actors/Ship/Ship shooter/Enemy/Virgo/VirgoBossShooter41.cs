using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static CameraBoundaries;

public class VirgoBossShooter41 : BossShooter<BossBullet>
{
    protected override float ShootingCooldown => 1.5f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(3f);

        for (int i = 1; enabled; i *= -1)
        {
            float x = i * ScreenHalfWidth * Random.Range(0.5f, 0.8f);
            float y = ScreenHalfHeight * 1.1f;
            float z = 0f;

            Vector3 pos = new(x, y);
            SpawnProjectile(1, z, pos, false).Fire();

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}