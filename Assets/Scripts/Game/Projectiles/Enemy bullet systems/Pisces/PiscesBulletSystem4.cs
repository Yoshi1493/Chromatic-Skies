using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem4 : EnemyShooter<EnemyBullet>
{
    const int BranchCount = 6;
    const float BranchSpacing = 360f / BranchCount;
    const float BranchWidth = 0.2f;
    const float ScaleFactor = 0.2f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();
        SetSubsystemEnabled(1);

        while (enabled)
        {
            for (int i = 0; i < BranchCount; i++)
            {
                float z = i * BranchSpacing;
                Vector3 pos = Vector3.zero;

                SpawnProjectile(1, z, pos).Fire();
                yield return WaitForSeconds(0.1f);
            }

            for (int i = 3; i < 8; i++)
            {
                SpawnProjectiles(i);
                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return CreateBranch(6, BranchWidth, 1.5f, 30f);
            yield return CreateBranch(10, BranchWidth, 2.2f, 30f);

            for (int i = 12; i < 16; i++)
            {
                SpawnProjectiles(i);
                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return CreateBranch(5, BranchWidth, 3.0f, 30f);
            StartCoroutine(CreateBranch(5, BranchWidth * 5, 3.4f, 90f));
            yield return CreateBranch(5, BranchWidth, 3.8f, 30f);

            for (int i = 20; i < 24; i++)
            {
                SpawnProjectiles(i);
                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return CreateBranch(4, BranchWidth, 4.6f, 30f);
            yield return CreateBranch(8, BranchWidth * 4, 5.0f, 120f);

            yield return WaitForSeconds(5f);

            StartMoveAction?.Invoke();
            yield return WaitForSeconds(2f);
        }
    }

    void SpawnProjectiles(int loopCount)
    {
        for (int i = 0; i < BranchCount; i++)
        {
            float z = i * BranchSpacing + 30f;

            Vector3 pos = new Vector3(BranchWidth, loopCount * ScaleFactor).RotateVectorBy(z);
            bulletData.colour = bulletData.gradient.Evaluate(pos.magnitude / 6f);
            SpawnProjectile(0, z, pos).Fire();

            pos = new Vector3(-BranchWidth, loopCount * ScaleFactor).RotateVectorBy(z);
            SpawnProjectile(0, z, pos).Fire();
        }
    }

    IEnumerator CreateBranch(int branchLength, float xOffset, float yOffset, float branchAngle)
    {
        for (int i = 1; i < branchLength; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float x = Mathf.Cos(branchAngle * Mathf.Deg2Rad) * i * ScaleFactor + xOffset;
                float y = Mathf.Sin(branchAngle * Mathf.Deg2Rad) * i * ScaleFactor + yOffset;

                float z = ii * BranchSpacing + 30f;
                float t = 90f - branchAngle;

                Vector3 pos = new Vector3(x, y).RotateVectorBy(z);
                bulletData.colour = bulletData.gradient.Evaluate(pos.magnitude / 6f);
                SpawnProjectile(0, z - t, pos).Fire();

                pos = new Vector3(-x, y).RotateVectorBy(z);
                SpawnProjectile(0, z + t, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}