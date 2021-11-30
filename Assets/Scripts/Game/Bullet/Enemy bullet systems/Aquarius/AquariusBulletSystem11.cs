using System.Collections;
using UnityEngine;

public class AquariusBulletSystem11 : EnemyBulletSubsystem<EnemyBullet>
{
    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            float rand = Random.value + 1f;
            yield return ownerShip.MoveToRandomPosition(1f, minSqrMagDelta: 1f, maxSqrMagDelta: 2f, rand);
        }
    }
}