using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem52 : EnemyShooter<Laser>
{
    const int LaserCount = 2;
    const float LaserSpacing = 60f;
    public const float LaserMaxLifetime = 5f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2f);

        for (int i = 0; i < LaserCount; i++)
        {
            float r = (i % 2 * 2 - 1) * LaserSpacing;
            float z = transform.position.GetRotationDifference(PlayerPosition) + r;
            Vector3 pos = Vector3.zero;

            var laser = SpawnProjectile(1, z, pos);
            laser.Fire();
            laser.StartCoroutine(laser.RotateBy(r * 0.9f, LaserMaxLifetime, delay: 0.5f));
        }

        enabled = false;
    }
}