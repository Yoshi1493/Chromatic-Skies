using System.Collections;
using UnityEngine;

public class TaurusBullet52 : ScriptableEnemyBullet<TaurusBulletSystem51, Laser>
{
    const int LaserCount = 4;
    const float LaserSpacing = 360f / LaserCount;

    protected override float MaxLifetime => 5f;

    protected override IEnumerator Move()
    {
        float r = transform.position.GetRotationDifference(playerShip.transform.position);

        for (int i = 0; i < LaserCount; i++)
        {
            float z = (i * LaserSpacing) + r;
            Vector3 pos = transform.position;

            SpawnBullet(0, z, pos, false).Fire();
        }

        yield break;
    }
}