using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem2 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 111;
    const int BranchCount = 6;
    const float BranchSpacing = 360f / BranchCount;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            for (int i = 0; i < BulletCount; i++)
            {
                float rand = Random.value * 360;
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = ii * BranchSpacing + rand;
                    SpawnProjectile(0, z, Vector2.zero).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(3f);
            StartCoroutine(ownerShip.MoveToRandomPosition(1f));

            for (int i = 0; i < 4; i++)
            {
                for (int ii = 0; ii < 12; ii++)
                {
                    float z = ii * 30f;

                    SpawnProjectile(1, z, Vector2.zero).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown * 2f);
            }
        }
    }
}