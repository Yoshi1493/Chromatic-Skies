using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem1 : EnemyBulletSystem
{
    Vector3 rotationAmount = 48 * Vector3.forward;

    protected override IEnumerator Start()
    {
        yield return base.Start();

        while (enabled)
        {
            for (int i = 0; i < spawnPositions.Count; i++)
            {
                SpawnBullet(0, i);
                spawnPositions[i].Rotate(rotationAmount);
            }

            yield return WaitForSeconds(ShootingCooldown * 5);
        }
    }
}