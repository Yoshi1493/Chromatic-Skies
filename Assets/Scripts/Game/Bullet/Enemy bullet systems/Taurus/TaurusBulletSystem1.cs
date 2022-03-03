using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem1 : EnemyShooter<EnemyBullet>
{
    List<EnemyBullet> outerBullets = new List<EnemyBullet>(90);
    List<EnemyBullet> innerBullets = new List<EnemyBullet>(90);

    readonly int OuterBranchCount = 5;
    readonly int OuterBranchDensity = 18;
    readonly int InnerBranchCount = 5;
    readonly float InnerBranchDensity = 18f;
    readonly float BranchSize = 2.5f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        while (enabled)
        {
            float randDirection = Mathf.Sign(Random.value - 0.5f);

            for (int i = 0; i < OuterBranchDensity; i++)
            {
                for (int j = 0; j < OuterBranchCount; j++)
                {
                    float z = ((i * 4f) + (j * 72f)) * randDirection;
                    var bullet = SpawnProjectile(0, z, Vector3.zero);
                    outerBullets.Add(bullet);

                    bullet.StartCoroutine(bullet.LerpSpeed(BranchSize * 2f, 0f, 1f));
                }

                yield return WaitForSeconds(ShootingCooldown / 4f);
            }

            yield return WaitForSeconds(1f);

            float xRange = Mathf.Cos(18f * Mathf.Deg2Rad) * BranchSize;
            float randOffset = Random.Range(0f, 36f);

            for (int i = 0; i < InnerBranchDensity; i++)
            {
                for (int j = 0; j < InnerBranchCount; j++)
                {
                    float lerpAmount = i / InnerBranchDensity;

                    float xOffset = Mathf.Lerp(-xRange, xRange, lerpAmount);
                    float yOffset = Mathf.Sin(18f * Mathf.Deg2Rad) * BranchSize;
                    Vector3 spawnPos = new Vector3(xOffset, yOffset).RotateVectorBy(randOffset + (j * 72f));

                    float z = Mathf.Lerp(-180f, 180f, lerpAmount);

                    var bullet = SpawnProjectile(1, z, spawnPos);
                    innerBullets.Add(bullet);

                }

                yield return WaitForSeconds(ShootingCooldown / 2f);
            }

            yield return WaitForSeconds(ShootingCooldown);

            for (int i = 0; i < outerBullets.Count; i++)
            {
                outerBullets[i].MoveSpeed = Random.Range(2f, 4f);
                outerBullets[i].Fire();
            }

            outerBullets.Clear();

            for (int i = 0; i < innerBullets.Count; i++)
            {
                innerBullets[i].Fire();
            }

            innerBullets.Clear();

            yield return ownerShip.MoveToRandomPosition(1f, delay: 1f);
        }
    }
}