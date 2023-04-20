using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        SetSubsystemEnabled(1);

        BezierCurve[] followPaths = GetComponentsInChildren<BezierCreator>().Select(i => i.curve).ToArray();
        int curveCount = followPaths.Length;

        while (enabled)
        {
            for (int i = 0; i < curveCount; i++)
            {
                var points = followPaths[i].CalculateEvenlySpacedPoints(BulletSpacing);
                int waveCount = points.Length;

                bullets = new(waveCount * BranchCount * BulletCount);

                for (int ii = 0; ii < waveCount; ii++)
                {
                    for (int iii = 0; iii < BranchCount; iii++)
                    {
                        for (int iv = 0; iv < BulletCount; iv++)
                        {
                            int b = i * BulletCount + iv;
                            float t = Vector3.zero.GetRotationDifference(points[ii].normal);
                            float z = iii * BranchSpacing;
                            Vector3 pos = ((Vector3)points[ii].position).RotateVectorBy(z);

                            bullets.Add(SpawnProjectile(b, z + t + (iv * 180f), pos));
                        }
                    }

                    yield return WaitForSeconds(ShootingCooldown);
                }

                yield return WaitForSeconds(0.5f);

                bullets.ForEach(b => b.Fire());
                bullets.Clear();

                yield return WaitForSeconds(0.5f);
            }

            StartMoveAction?.Invoke();
        }
    }
}