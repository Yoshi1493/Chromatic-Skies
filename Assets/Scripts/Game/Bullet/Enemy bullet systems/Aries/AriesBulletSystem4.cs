using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem4 : EnemyBulletSystem
{
    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        EnableSubsystem(1);
        
        yield return WaitForSeconds(2f);

        for (int i = 0; i < 5; i++)
        {
            float z = i * 144f;
            Vector3 currOffset = transform.up.RotateVectorBy(z);
            Vector3 nextOffset = transform.up.RotateVectorBy(z + 144f);

            for (int j = 0; j < 10; j++)
            {
                Vector3 lerpOffset = Vector3.Lerp(currOffset, nextOffset, j / 10f);
                SpawnBullet(1, (transform.position + lerpOffset).GetRotationDifference(transform.position), lerpOffset);

                yield return WaitForSeconds(ShootingCooldown / 10f);
            }
        }
    }
}