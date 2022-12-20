using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class AquariusBulletSystem2 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 40;
    const float WaveSpacing = 0.25f;
    const float WaveFrequency = 15f;
    const int BranchCount = 8;
    const float BranchSpacing = 360f / BranchCount;

    List<EnemyBullet> bullets = new(WaveCount * BranchCount);

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();
        SetSubsystemEnabled(1);

        while (enabled)
        {
            int d = PositiveOrNegativeOne;

            for (int i = 1; i <= WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float x = Mathf.Sin(i * WaveFrequency * d * Mathf.Deg2Rad);
                    float y = i * WaveSpacing;
                    float z = ii * BranchSpacing;
                    Vector3 pos = new Vector3(x, y).RotateVectorBy(z);

                    var bullet = SpawnProjectile(0, z + (x * BranchSpacing), pos);
                    bullets.Add(bullet);
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(1f);

            bullets.ForEach(b => b.Fire());
            bullets.Clear();

            //yield return ownerShip.MoveToRandomPosition(1f, delay: 4f);
            yield return WaitForSeconds(1f);
        }
    }
}