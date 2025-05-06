using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static CameraBoundaries;

public class AriesBossShooter41 : BossShooter<BossBullet>
{
    const int AngleLimit = 90;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2f);

        float y = ScreenHalfHeight + 1f;

        for (int i = Random.value > 0.5f ? 0 : AngleLimit; enabled; i++)
        {
            float x = Random.Range(-ScreenHalfWidth, ScreenHalfWidth);
            float z = Mathf.PingPong(i, AngleLimit) - (AngleLimit * 0.5f);
            Vector3 pos = new(x, y);

            SpawnProjectile(1, z, pos, false).Fire();
            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}