using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem31 : EnemyShooter<Laser>
{
    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(5f);

        for (int i = 0; i < 32; i++)
        {
            float z = Mathf.PingPong(i, 8) * 10f - 40f + (i / 8 * 5 - 7.5f);
            SpawnProjectile(1, z, Vector3.zero);

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}