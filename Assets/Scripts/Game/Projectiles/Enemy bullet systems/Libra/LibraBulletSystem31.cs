using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem31 : EnemyShooter<EnemyBullet>
{
    const int MinSize = 3;
    const int MaxSize = 6;
    const float Spacing = 1f;

    protected override float ShootingCooldown => 0.3f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(ShootingCooldown);

        while (enabled)
        {
            int randSize = Random.Range(MinSize, MaxSize);
            float z = PlayerPosition.GetRotationDifference(transform.position);

            for (int i = 0; i <= randSize; i++)
            {
                float xOffset = (i - 1) * Spacing / 2f;

                for (int j = 0; j < i; j++)
                {
                    Vector3 pos = ((j * Spacing - xOffset) * Vector3.right).RotateVectorBy(z);
                    bulletData.colour = bulletData.gradient.Evaluate(i / (randSize - 1f));

                    SpawnProjectile(0, z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }
        }

    }
}