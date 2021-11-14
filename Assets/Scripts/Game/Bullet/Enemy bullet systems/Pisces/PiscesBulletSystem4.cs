using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem4 : EnemyShooter<EnemyBullet>
{
    [SerializeField] ProjectileObject bulletData;

    protected override float ShootingCooldown => base.ShootingCooldown / 2;

    readonly int BranchCount = 6;
    readonly float BranchWidth = 0.2f;
    readonly float ScaleFactor = 0.2f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            StartCoroutine(CreateBranch(5, BranchWidth, 1.4f, 30f));
            StartCoroutine(CreateBranch(9, BranchWidth, 2.2f, 30f));
            StartCoroutine(CreateBranch(5, BranchWidth, 3.0f, 30f));
            StartCoroutine(CreateBranch(5, BranchWidth * 5, 3.4f, 90f));
            StartCoroutine(CreateBranch(5, BranchWidth, 3.8f, 30f));
            StartCoroutine(CreateBranch(4, BranchWidth, 4.6f, 30f));
            StartCoroutine(CreateBranch(8, BranchWidth * 4, 5.0f, 120f));

            for (int i = -1; i < 12; i++)
            {
                for (int j = 0; j < BranchCount; j++)
                {
                    float xPos = BranchWidth;
                    float yPos = ScaleFactor * (i + 4 * (i / 4) + 4);

                    float zRot = j * 60f;



                    SpawnProjectile(0, zRot, new Vector3(xPos, yPos).RotateVectorBy(zRot)).Fire();
                    SpawnProjectile(0, zRot, new Vector3(-xPos, yPos).RotateVectorBy(zRot)).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(10f);
        }
    }

    IEnumerator CreateBranch(int branchLength, float xOffset, float yOffset, float branchAngle)
    {
        yield return WaitForSeconds(ShootingCooldown * (yOffset / 1f));

        for (int i = 1; i < branchLength; i++)
        {
            for (int j = 0; j < BranchCount; j++)
            {
                float xPos = Mathf.Cos(branchAngle * Mathf.Deg2Rad) * i * ScaleFactor + xOffset;
                float yPos = Mathf.Sin(branchAngle * Mathf.Deg2Rad) * i * ScaleFactor + yOffset;

                float zRot = 90 - branchAngle;
                float theta = j * 360 / BranchCount;

                SpawnProjectile(0, theta - zRot, new Vector3(xPos, yPos).RotateVectorBy(theta)).Fire();
                SpawnProjectile(0, theta + zRot, new Vector3(-xPos, yPos).RotateVectorBy(theta)).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }

    /*
    IEnumerator CreateBranch(int startingPoint, int branchLength, float branchAngle, bool completeBranch = false)
    {
        yield return WaitForSeconds(ShootingCooldown * startingPoint);
        float centreOffset = 0.2f + (startingPoint / 10f);

        for (int i = 1; i < branchLength; i++)
        {
            for (int j = 0; j < BranchCount; j++)
            {
                float xPos = Mathf.Sin(branchAngle * Mathf.Deg2Rad) * ScaleFactor * i + BranchWidth;
                float yPos = Mathf.Cos(branchAngle * Mathf.Deg2Rad) * ScaleFactor * (i + startingPoint) + centreOffset;

                float zRot = -branchAngle;
                float theta = j * 60f;

                SpawnProjectile(0, theta + zRot, new Vector3(xPos, yPos).RotateVectorBy(theta)).Fire();
                SpawnProjectile(0, theta - zRot, new Vector3(-xPos, yPos).RotateVectorBy(theta)).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        if (completeBranch)
        {
            for (int j = 0; j < BranchCount; j++)
            {
                float xPos = Mathf.Sin(branchAngle * Mathf.Deg2Rad) * ScaleFactor * branchLength + BranchWidth;
                float yPos = Mathf.Cos(branchAngle * Mathf.Deg2Rad) * ScaleFactor * (branchLength + startingPoint) + centreOffset;

                float zRot = -branchAngle / 2f;
                float theta = j * 60f;

                SpawnProjectile(0, theta + zRot, new Vector3(xPos, yPos).RotateVectorBy(theta)).Fire();
            }
        }
    }
    */
}