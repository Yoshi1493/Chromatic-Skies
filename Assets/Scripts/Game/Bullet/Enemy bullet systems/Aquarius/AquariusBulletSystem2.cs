using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class AquariusBulletSystem2 : EnemyShooter<EnemyBullet>
{
    const int BranchCount = 8;
    const int BranchSpacing = 360 / BranchCount;
    const int BulletCount = 40;
    const float BulletSpacingMultiplier = 0.4f;
    const float Frequency = 15f;

    List<EnemyBullet> bullets = new List<EnemyBullet>(BranchCount * BulletCount);

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();
        SetSubsystemEnabled(1);

        while (enabled)
        {
            yield return WaitForSeconds(1f);

            float rand = Mathf.Sign(Random.value - 0.5f);

            for (int i = 1; i <= BulletCount; i++)
            {
                for (int j = 0; j < BranchCount; j++)
                {
                    float x = Mathf.Sin(i * Frequency * rand * Mathf.Deg2Rad);
                    float y = i * BulletSpacingMultiplier;

                    float z = j * BranchSpacing;

                    var bullet = SpawnProjectile(0, z + (x * BranchSpacing), new Vector3(x, y).RotateVectorBy(z));
                    bullets.Add(bullet);
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            bullets.ForEach(b => b.Fire());
            bullets.Clear();

            yield return ownerShip.MoveToRandomPosition(1f, delay: 4f);
        }
    }
}