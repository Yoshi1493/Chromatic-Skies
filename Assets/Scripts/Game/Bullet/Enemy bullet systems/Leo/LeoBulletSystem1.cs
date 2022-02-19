using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem1 : EnemyShooter<EnemyBullet>
{
    readonly float Spacing = 1.2f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            SetSubsystemEnabled(1);

            float rand = Random.Range(0f, 60f);

            for (int i = 1; i <= 6; i++)
            {
                float xOffset = (i - 1) * Spacing / 2f;

                for (int j = 0; j < i; j++)
                {
                    Vector3 offset = (j * Spacing - xOffset) * Vector3.right;

                    for (int k = 0; k < 6; k++)
                    {
                        float z = k * 60f + rand;
                        SpawnProjectile(0, z, offset.RotateVectorBy(z)).Fire();
                    }
                }

                yield return WaitForSeconds(ShootingCooldown * 5f);
            }

            yield return ownerShip.MoveToRandomPosition(1f, delay: 4f);
        }
    }
}