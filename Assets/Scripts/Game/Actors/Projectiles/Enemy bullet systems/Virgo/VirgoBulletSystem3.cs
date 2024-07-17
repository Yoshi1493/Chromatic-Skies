using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem3 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 100;
    const float AngularFrequency = 2f / 7f;
    const float WaveSpacing = 1f / (WaveCount * AngularFrequency);
    const int BranchCount = 4;
    const float BranchSpacing = 360f / BranchCount;
    const float RingRadius = 2.5f;

    List<Vector3> bulletSpawnData = new(WaveCount * BranchCount);

    protected override float ShootingCooldown => 1f / 60;

    protected override void Awake()
    {
        base.Awake();

        //rhodonea (rose) curve `f(É∆) = r*sin(kÉ∆)`, where
        //r = radius
        //É∆ = anticlockwise rotation amount on polar coordinates from the origin
        //k = angular frequency, expressed in the form n/d
        for (int i = 0; i < WaveCount; i++)
        {
            int d = i % 2 * 2 - 1;

            for (int ii = 0; ii < BranchCount; ii++)
            {
                float t = i * WaveSpacing;
                float r = RingRadius * Mathf.Sin(t * AngularFrequency * Mathf.PI);

                Vector3 pos = (r * transform.up.RotateVectorBy(t * 180f)).RotateVectorBy(ii * BranchSpacing);
                float z = (d * 90f) + pos.GetRotationDifference(Vector3.zero);

                bulletSpawnData.Add(new(pos.x, pos.y, z));
            }
        }
    }

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    int b = (i * BranchCount) + ii;
                    Vector3 data = bulletSpawnData[b];

                    SpawnProjectile(0, data.z, (Vector2)data).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            SetSubsystemEnabled(1);
            SetSubsystemEnabled(2);
            StartMoveAction?.Invoke();

            yield return WaitForSeconds(6f);
        }
    }
}