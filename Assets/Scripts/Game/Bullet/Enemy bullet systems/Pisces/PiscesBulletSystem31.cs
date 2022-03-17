using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem31 : EnemyBulletSubsystem<EnemyBullet>
{
    protected override float ShootingCooldown => 0.05f;
    const int BulletCount = 60;
    const int BranchCount = 3;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(3f);

        for (int i = 0; i < BulletCount; i++)
        {
            float r = Random.Range(0, 120f);

            for (int j = 0; j < BranchCount; j++)
            {
                float z = r + (j * 120f);
                SpawnProjectile(1, z, 0.5f * transform.up.RotateVectorBy(z + 90)).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}