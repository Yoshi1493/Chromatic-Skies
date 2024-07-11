using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem5 : EnemyShooter<EnemyBullet>
{
    const int RingCount = 16;
    const int BranchCount = 5;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 5;
    const float BulletSpacing = 5f;
    const float BulletSpawnRadius = 2f;
    const float SpawnRadiusModifier = 0.5f;


    List<EnemyBullet> bullets = new(RingCount);

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        //StartMoveAction?.Invoke();

        for (int i = 0; i < RingCount; i++)
        {

        }
    }
}