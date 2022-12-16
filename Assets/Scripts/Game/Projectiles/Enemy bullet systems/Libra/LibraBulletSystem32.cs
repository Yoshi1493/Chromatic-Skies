using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem32 : EnemyShooter<EnemyBullet>
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
                Vector3 pos = 0.5f * transform.up.RotateVectorBy(z);

                SpawnProjectile(Random.value > Mathf.PingPong(bigBulletChance, 1f) ? 1 : 2, z, pos).Fire();

                i++;
                yield return WaitForSeconds(ShootingCooldown);
            }

            bigBulletChance += 0.1f;
        }
    }
}