using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem32 : EnemyBulletSubsystem<EnemyBullet>
{
    readonly float goldenRatio = (1f + Mathf.Sqrt(5f)) * 180f;

    protected override float ShootingCooldown => 0.03f;

    protected override IEnumerator Shoot()
    {
        int i = 0;
        float bigBulletChance = 0f;

        while (enabled)
        {
            for (int j = 0; j < 30; j++)
            {
                float z = i * goldenRatio;
                SpawnProjectile(Random.value > Mathf.PingPong(bigBulletChance, 1f) ? 1 : 2, z, transform.up.RotateVectorBy(z) * 0.5f).Fire();

                i++;
                yield return WaitForSeconds(ShootingCooldown);
            }

            bigBulletChance += 0.1f;
        }
    }
}