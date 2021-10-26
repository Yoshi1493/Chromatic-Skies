using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem11 : EnemyShooter<EnemyBullet>
{
    [SerializeField] Vector3[] bigBulletSpawnPos;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            for (int i = 0; i < bigBulletSpawnPos.Length; i++)
            {
                SpawnProjectile(1, bigBulletSpawnPos[i].z, bigBulletSpawnPos[i], false).Fire();
            }

            yield return WaitForSeconds(8f);
        }
    }
}