using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem1 : EnemyBulletSystem
{
    Vector3 rotationAmount = 15 * Vector3.forward;

    IEnumerator Start()
    {
        EnemyBulletPool.Instance.UpdatePoolableBullets(enemyBullets);
        yield return WaitForSeconds(1f);

        spawnPositions[1].Rotate(rotationAmount / 2);

        while (enabled)
        {
            for (int i = 0; i < spawnPositions.Count; i++)
            {
                SpawnBullet(0, i);
                yield return WaitForSeconds(ShootingCooldown);
            }

            transform.Rotate(rotationAmount);
        }
    }
}