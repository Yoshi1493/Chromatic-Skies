using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem4 : EnemyShooter<EnemyBullet>
{
    const int MinGridLength = 3;
    const int MaxGridLength = 6;
    const float MaxRandomSpawnOffset = 5f;
    const float BulletSpacing = 1f;

    readonly Vector3[] bulletSpawnDirections = new Vector3[]
    {
        new(-1,  0),
        new( 0,  1),
        new( 1,  0),
        new( 0, -1)
    };

    protected override float ShootingCooldown => 0.8f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        StartMoveAction?.Invoke();

        for (int i = 0; enabled; i++)
        {
            i %= bulletSpawnDirections.Length;

            int gridLength = Random.Range(MinGridLength, MaxGridLength);

            float z = Vector3.zero.GetRotationDifference(bulletSpawnDirections[i]);
            Vector3 v1 = new(bulletSpawnDirections[i].x * (screenHalfWidth + (gridLength / 2f)), bulletSpawnDirections[i].y * (screenHalfHeight + (gridLength / 2f)));
            Vector3 v2 = Random.Range(-MaxRandomSpawnOffset, MaxRandomSpawnOffset + 1) * new Vector3(bulletSpawnDirections[i].y, bulletSpawnDirections[i].x);
            
            bulletData.colour = bulletData.gradient.Evaluate(i / (bulletSpawnDirections.Length - 1f));

            for (int ii = 0; ii < gridLength; ii++)
            {
                for (int iii = 0; iii < gridLength; iii++)
                {
                    float x = (iii - ((gridLength - 1) / 2f)) * BulletSpacing;
                    float y = (ii - ((gridLength - 1) / 2f)) * BulletSpacing;
                    Vector3 pos = v1 + v2 + new Vector3(x, y).RotateVectorBy(z);

                    SpawnProjectile(0, z, pos, false).Fire();
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}