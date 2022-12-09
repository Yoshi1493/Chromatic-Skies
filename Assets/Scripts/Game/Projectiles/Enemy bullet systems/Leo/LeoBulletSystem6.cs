using System.Collections;
using static CoroutineHelper;

public class LeoBulletSystem6 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 24;
    const float WaveSpacing = 360f / WaveCount;
    const int BranchCount = 15;
    const float BranchSpacing = 360f / BranchCount;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                for (int j = 0; j < BranchCount; j++)
                {
                    float z = (i * WaveSpacing) + (j * BranchSpacing);

                    SpawnProjectile(0, z, transform.up.RotateVectorBy(-z)).Fire();
                    SpawnProjectile(1, -z, transform.up.RotateVectorBy(z)).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return ownerShip.MoveToRandomPosition(1f);
        }
    }
}