using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem3 : EnemyShooter<EnemyBullet>
{
    readonly float amp = 2f;
    readonly int n = 2;
    readonly int d = 7;
    
    List<EnemyBullet> bullets = new();

    protected override float ShootingCooldown => 0.01f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            //rhodonea curve r = sin(theta * n / d), where
            //theta = anticlockwise rotation amount on polar coordinates from point(1, 0)
            //n / d = angular frequency
            for (float i = 0f; i < d; i += 0.035f)
            {
                float theta = i * Mathf.PI;
                float r = Mathf.Sin(theta * n / d);

                Vector3 spawnOffset = transform.right.RotateVectorBy(theta * Mathf.Rad2Deg);
                float z = spawnOffset.GetRotationDifference(Vector3.zero);

                bullets.Add(SpawnProjectile(0, z, r * amp * spawnOffset));
                bullets.Add(SpawnProjectile(0, z + 180f, -r * amp * spawnOffset));

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(1f);

            bullets.ForEach(b => b.Fire());
            bullets.Clear();

            yield return WaitForSeconds(2f);

            SetSubsystemEnabled(1);

            yield return WaitForSeconds(3f);

            yield return ownerShip.MoveToRandomPosition(1f, delay: 1f);
        }
    }
}