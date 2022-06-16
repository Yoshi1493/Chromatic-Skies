using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem3 : EnemyShooter<EnemyBullet>
{
    const float RingRadius = 2.5f;
    const int d = 7;
    const float AngularFrequency = 2f / 7f;
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 200;
    int counter;
    List<EnemyBullet> bullets = new(BranchCount * BulletCount);

    protected override float ShootingCooldown => 0.01f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        //SetSubsystemEnabled(1);

        const float step = d / (float)BulletCount;

        while (enabled)
        {
            yield return WaitForSeconds(1f);

            //rhodonea curve r = sin(theta * n / d), where
            //theta = anticlockwise rotation amount on polar coordinates from point(1, 0)
            //n / d = angular frequency
            for (float i = step; i < d; i += step)
            {
                Vector3 pos = transform.right.RotateVectorBy(i * 180f);
                float z = pos.GetRotationDifference(Vector3.zero);
                float r = RingRadius * Mathf.Sin(i * AngularFrequency * Mathf.PI);

                bullets.Add(SpawnProjectile(0, z, r * pos));
                bullets.Add(SpawnProjectile(0, z + 180f, -r * pos));
                counter+= 2;

                yield return WaitForSeconds(ShootingCooldown);
            }

            print(counter);
            yield return WaitForSeconds(1f);

            bullets.ForEach(b => b.Fire());
            bullets.Clear();

            yield return ownerShip.MoveToRandomPosition(1f, delay: 1f);
        }
    }
}