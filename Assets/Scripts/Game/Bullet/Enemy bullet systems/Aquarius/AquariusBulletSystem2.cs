using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class AquariusBulletSystem2 : EnemyShooter<EnemyBullet>
{
    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            for (int i = 1; i < 36; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    float xPos = 2f * Mathf.Abs(Mathf.Sin(i * 15 * Mathf.Deg2Rad));
                    float yPos = i / 3f;

                    float zRot = j * 45f;

                    SpawnProjectile(0, zRot + (xPos * 45f), transform.InverseTransformVector(xPos, yPos, 0f).RotateVectorBy(zRot)).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown / 2f);
            }

            SetSubsystemEnabled(1);

            yield return ownerShip.MoveToRandomPosition(1f, delay: 5f);
        }
    }
}