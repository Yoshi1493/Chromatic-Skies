using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem1 : EnemyShooter<EnemyBullet>
{
    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        yield return WaitForSeconds(1f);

        while (enabled)
        {
            StartCoroutine(ownerShip.MoveToRandomPosition(0.5f));

            for (int i = 0; i < 24; i++)
            {
                float z = i * 15f;
                SpawnProjectile(0, z, transform.up.RotateVectorBy(z)).Fire();
            }

            yield return WaitForSeconds(2f);
        }
    }
}