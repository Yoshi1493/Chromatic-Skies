using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class LeoBulletSystem21 : EnemyBulletSubsystem<EnemyBullet>
{
    const int BranchCount = 15;
    const int BulletCount = 6;
    const int BulletSpacing = 360 / BulletCount;

    Queue<EnemyBullet> bullets = new(BranchCount);

    //protected override float ShootingCooldown => 0.15f;

    protected override IEnumerator Shoot()
    {
        yield return EndOfFrame;

        while (enabled)
        {
            float x = (Random.Range(0f, camHalfWidth * 0.6f) + (camHalfWidth * 0.2f)) * PositiveOrNegativeOne;

            for (int i = 0; i < BranchCount; i++)
            {
                float y = Mathf.Lerp(camHalfHeight, -camHalfHeight, (float)i / BranchCount);
                Vector3 pos = new(x, y);

                var bullet = SpawnProjectile(2, 0, pos, false);
                bullet.Fire();
                bullets.Enqueue(bullet);

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(3f - (ShootingCooldown * BranchCount));

            for (int i = 0; i < BranchCount; i++)
            {
                var bullet = bullets.Dequeue();

                for (int j = 0; j < BulletCount; j++)
                {
                    float z = (i * BranchCount) + (j * BulletSpacing);
                    SpawnProjectile(3, z, bullet.transform.position, false).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return ownerShip.MoveToRandomPosition(1f, delay: 10f);
        }
    }
}