using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBullet50 : ScriptableEnemyBullet<TaurusBulletSystem52, Laser>
{
    const int LaserCount = 2;
    const float LaserSpacing = TaurusBulletSystem5.WaveSpacing;

    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(1f);

        for (int i = 0; i < LaserCount; i++)
        {
            float r = (i % 2 * 2 - 1) * (LaserSpacing / 4);
            float z = transform.eulerAngles.z + r;
            Vector3 pos = transform.position;

            var laser = SpawnBullet(0, z, pos, false);
            laser.transform.parent = transform;
            laser.Fire();
        }
    }
}