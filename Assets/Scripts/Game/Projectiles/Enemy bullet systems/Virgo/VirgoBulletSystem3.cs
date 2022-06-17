using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem3 : EnemyShooter<EnemyBullet>
{
    const float RingRadius = 2.5f;
    const int BulletCount = 200;
    const float AngularFrequency = 2f / 7f;
    const float AngularStep = 0.01f / AngularFrequency;

    List<EnemyBullet> bullets = new(BulletCount * 2);

    protected override float ShootingCooldown => 0.01f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        while (enabled)
        {
            yield return WaitForSeconds(1f);

            //rhodonea (rose) curve r = sin(k * theta), where
            //theta = anticlockwise rotation amount on polar coordinates from the origin
            //k = angular frequency, espressed in the form n/d
            for (float i = 0; i < BulletCount; i++)
            {
                Vector3 pos = transform.right.RotateVectorBy(i * AngularStep * 180f);

                float z = pos.GetRotationDifference(Vector3.zero);
                float d = RingRadius * Mathf.Sin(i * AngularStep * AngularFrequency * Mathf.PI);

                bullets.Add(SpawnProjectile(0, z, d * pos));
                bullets.Add(SpawnProjectile(0, z + 180f, -d * pos));

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(1f);

            bullets.ForEach(b => b.Fire());
            bullets.Clear();

            yield return ownerShip.MoveToRandomPosition(1f, delay: 1f);
        }
    }
}