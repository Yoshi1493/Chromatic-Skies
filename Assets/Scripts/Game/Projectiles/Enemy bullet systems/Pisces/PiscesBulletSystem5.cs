using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem5 : EnemyShooter<EnemyBullet>
{
    const float WaveSpacing = 10f;
    const int BulletCount = 8;
    const int BulletSpacing = 360 / BulletCount;

    protected override float ShootingCooldown => 0.8f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);
        StartCoroutine(Move());
        int i = 0;

        while (enabled)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = (i * WaveSpacing) + (ii * BulletSpacing);
                Vector3 pos = Vector3.zero;

                SpawnProjectile(0, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
            i++;
        }
    }

    IEnumerator Move()
    {
        while (enabled)
        {
            int r = Random.Range(4, 7);
            yield return WaitForSeconds(r);
            //yield return ownerShip.MoveToRandomPosition(1f, minSqrMagDelta: 1f, maxSqrMagDelta: 2f);
        }
    }
}