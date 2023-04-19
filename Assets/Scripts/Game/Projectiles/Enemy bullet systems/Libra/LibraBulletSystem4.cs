using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem4 : EnemyShooter<EnemyBullet>
{
    const int BranchCount = 4;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;
    const float BulletSpacing = 0.2f;

    List<EnemyBullet> bullets;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        BezierCurve followPath = GetComponent<BezierCreator>().curve;

        var points = followPath.CalculateEvenlySpacedPoints(BulletSpacing);
        int waveCount = points.Length;

        bullets = new(waveCount * BranchCount * BulletCount);

        while (enabled)
        {
            float r = Random.Range(0f, BranchSpacing);

            for (int i = 0; i < waveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        float t = Vector3.zero.GetRotationDifference(points[i].normal);
                        float z = (ii * BranchSpacing) + r;
                        Vector3 pos = ((Vector3)points[i].position).RotateVectorBy(z);

                        bulletData.colour = bulletData.gradient.Evaluate(i / (waveCount - 1f));
                        bullets.Add(SpawnProjectile(iii, z + t + (iii * 180f), pos));
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(0.5f);

            bullets.ForEach(b => b.Fire());
            bullets.Clear();

            yield return WaitForSeconds(10f);
        }
    }
}