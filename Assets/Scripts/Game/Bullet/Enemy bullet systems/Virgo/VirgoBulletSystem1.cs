using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem1 : EnemyBulletSystem
{
    protected override IEnumerator Shoot()
    {
        float goldenRatio = (1 + Mathf.Sqrt(5)) * 180;

        while (enabled)
        {
            yield return base.Shoot();

            SetSubsystemEnabled(1);

            //377 is the 14th number in the fibonacci sequence
            for (int i = 0; i < 377; i++)
            {
                float z = i * goldenRatio;
                SpawnProjectile(1, z, Vector2.zero).Fire();

                yield return WaitForSeconds(ShootingCooldown / 8f);
            }

            yield return ownerShip.MoveToRandomPosition(1f, 2f);
        }
    }
}