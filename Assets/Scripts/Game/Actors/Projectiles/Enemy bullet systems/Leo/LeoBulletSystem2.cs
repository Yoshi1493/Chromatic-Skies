using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem2 : EnemyShooter<EnemyBullet>
{
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 8;
    const float MaxBulletSpawnOffset = 30f;

    const float halfPI = 0.5f * Mathf.PI;

    List<Vector3> bulletPositions = new(BulletCount * 4 - 1);

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        //lemniscate (infinity curve) (x^2 + y^2)^2 - rx^2 + ry^2 = 0, modified and broken into 3 parts:
        //positive sine wave:  f(x) = sin(x)      {1 < x < t}
        //semicircle:          1 = (x-t)^2 + y^2  {x > t}
        //negative sine wave:  f(x) = -sin(x)     {-t < x < -1}
        //where t = pi / 2
        while (enabled)
        {
            //float r = Random.Range(-180f, 180f);
            //transform.eulerAngles = r * Vector3.forward;

            float t = halfPI / BulletCount;
            float x, y;

            bulletPositions.Clear();

            //positive sine wave
            for (int i = 1; i < BulletCount; i++)
            {
                x = i * t;
                y = Mathf.Sin(x);
                Vector3 pos = new(x, y);

                bulletPositions.Add(pos);
            }

            //semicircle
            for (int i = 0; i < BulletCount * 2; i++)
            {
                Vector3 offset = halfPI * Vector3.right;
                float theta = i * -90f / BulletCount;
                Vector3 pos = Vector3.up.RotateVectorBy(theta) + offset;

                bulletPositions.Add(pos);
            }

            //negative sine wave
            for (int i = BulletCount; i > 0; i--)
            {
                x = i * t;
                y = -Mathf.Sin(x);
                Vector3 pos = new(x, y);

                bulletPositions.Add(pos);
            }

            float r = Random.Range(-MaxBulletSpawnOffset, MaxBulletSpawnOffset);

            for (int i = 0; i < bulletPositions.Count; i++)
            {
                Vector3 pos = bulletPositions[i];

                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = pos.GetRotationDifference(Vector3.zero) + (ii * BranchSpacing) + r;

                    var bullet = SpawnProjectile(0, z, Vector3.zero);
                    bullet.MoveSpeed = bulletPositions[i].sqrMagnitude;
                    bullet.Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(2f);

            StartMoveAction?.Invoke();
            SetSubsystemEnabled(1);

            yield return WaitForSeconds(2f);
        }
    }
}