using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem3 : EnemyShooter<EnemyBullet>
{
    const float RingRadius = 2.5f;
    const int BulletCount = 100;
    const float AngularFrequency = 2f / 7f;
    const float AngularStep = 2f / BulletCount / AngularFrequency;

    protected override float ShootingCooldown => 0.02f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        while (enabled)
        {
            //rhodonea (rose) curve r = sin(k * theta), where
            //theta = anticlockwise rotation amount on polar coordinates from the origin
            //k = angular frequency, espressed in the form n/d
            for (float i = 0; i < BulletCount; i++)
            {
                Vector3 pos = transform.right.RotateVectorBy(i * AngularStep * 180f);

                float z = pos.GetRotationDifference(Vector3.zero);
                float d = RingRadius * Mathf.Sin(i * AngularStep * AngularFrequency * Mathf.PI);

                SpawnProjectile(0, z + 90f, d * pos).Fire();
                SpawnProjectile(0, z - 90f, -d * pos).Fire();

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return ownerShip.MoveToRandomPosition(1f, delay: 3f);
        }
    }
}