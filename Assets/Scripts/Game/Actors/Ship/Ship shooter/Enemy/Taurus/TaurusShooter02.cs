using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusShooter02 : CommonEnemyShooter<Laser>
{
    const int LaserCount = 2;
    const float LaserSpacing = 360f / LaserCount;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1.5f);

        float r = transform.position.GetRotationDifference(PlayerPosition);

        for (int i = 0; i < LaserCount; i++)
        {
            float z = (i * LaserSpacing) + r;
            Vector3 pos = Vector3.zero;

            SpawnProjectile(0, z, pos).Fire(1f);
        }
    }
}