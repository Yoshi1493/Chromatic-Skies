using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem4 : EnemyShooter<EnemyBullet>
{
    const int BranchCount = 4;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;
    const float BulletSpacing = 0.2f;

    protected override float ShootingCooldown => 0.04f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        BezierCurve followPath = GetComponent<BezierCreator>().curve;

        var points = followPath.CalculateEvenlySpacedPoints(BulletSpacing);
        int waveCount = points.Length;

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
                        SpawnProjectile(iii, z + t + (iii * 180f), pos).Fire();
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(1f);

            StartMoveAction?.Invoke();

            yield return WaitForSeconds(2f);
        }
    }
}