using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem41 : EnemyShooter<Laser>
{
    LeoBulletSystem4 bulletSystem;

    const int LaserCount = 5;
    const int BranchCount = 2;
    const float LaserSpacing = 0.64f;

    List<Vector3> clonePositions;

    protected override float ShootingCooldown => 0.2f;

    protected override void Awake()
    {
        base.Awake();
        bulletSystem = GetComponentInParent<LeoBulletSystem4>();
    }

    protected override IEnumerator Shoot()
    {
        //yield return WaitForSeconds(2f);

        clonePositions = GetComponentInParent<LeoBulletSystem4>().bulletSpawnPositions;

        for (int i = 0; i < clonePositions.Count; i++)
        {
            for (int ii = 0; ii < LaserCount; ii++)
            {
                for (int iii = 0; iii < BranchCount; iii++)
                {
                    float x = clonePositions[i].x + ((iii % 2 * 2 - 1) * ii  * LaserSpacing);
                    float y = screenHalfHeight * 1.1f;
                    float z = 180f;
                    Vector3 pos = new(x, y);

                    bulletData.colour = bulletData.gradient.Evaluate(ii / (LaserCount - 1f));

                    SpawnProjectile(0, z, pos, false).Fire(1f);
                }

                yield return WaitForSeconds(ShootingCooldown);
            }
        }

        enabled = false;
    }
}