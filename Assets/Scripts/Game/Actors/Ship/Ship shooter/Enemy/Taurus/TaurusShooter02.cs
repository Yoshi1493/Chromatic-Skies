using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusShooter02 : CommonEnemyShooter<Laser>
{
    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1.5f);

        float z = transform.position.GetRotationDifference(PlayerPosition);
        Vector3 pos = Vector3.zero;

        SpawnProjectile(0, z, pos).Fire(1f);
    }
}