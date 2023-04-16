using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem4 : EnemyShooter<EnemyBullet>
{
    protected override float ShootingCooldown => 0.05f;
    const float BulletSpacing = 0.2f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        BezierCurve followPath = GetComponent<BezierCreator>().curve;

        var points = followPath.CalculateEvenlySpacedPoints(BulletSpacing);

        while (enabled)
        {
            for (int i = 0; i < points.Length; i++)
            {
                float z = Vector3.zero.GetRotationDifference(points[i].normal);
                Vector3 pos = points[i].position;

                SpawnProjectile(0, z, pos).Fire();


                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(10f);
        }
    }
}