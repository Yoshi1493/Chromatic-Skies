using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem11 : EnemyBulletSubsystem<EnemyBullet>
{
    [SerializeField] Vector3[] bulletSpawnPos;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            for (int i = 0; i < bulletSpawnPos.Length; i++)
            {
                SpawnProjectile(1, bulletSpawnPos[i].z, bulletSpawnPos[i], false).Fire();
            }

            yield return WaitForSeconds(8f);
        }
    }
}