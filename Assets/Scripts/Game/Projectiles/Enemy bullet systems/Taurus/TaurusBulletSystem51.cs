using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem51 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 4;
    const int BranchCount = 10;
    const float BranchSpacing = 5f;
    const int BulletCount = 3;
    const float BulletBaseSpeed = 2.25f;
    const float BulletSpeedModifier = 0.75f;

    protected override IEnumerator Shoot()
    {
       yield return enabled = false;
    }
}