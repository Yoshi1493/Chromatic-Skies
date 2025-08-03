using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusShooter03 : CommonEnemyShooter<EnemyBullet>
{
    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2f);

        float z = PlayerPosition.GetRotationDifference(transform.position);
        Vector3 pos = Vector3.zero;

        SpawnProjectile(3, z, pos).Fire();
    }
}