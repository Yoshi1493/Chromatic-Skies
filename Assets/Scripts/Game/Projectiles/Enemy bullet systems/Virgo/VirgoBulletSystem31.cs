using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem31 : EnemyShooter<EnemyBullet>
{
    const int BranchCount = 4;
    const int BulletCount = 8;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i <= BranchCount * BulletCount; i++)
        {
            float z = Mathf.PingPong(i, BulletCount) * 10f - 40f + (i / 8 * 5 - 7.5f);
            print(z);
            SpawnProjectile(1, z, Vector3.zero).Fire();

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}