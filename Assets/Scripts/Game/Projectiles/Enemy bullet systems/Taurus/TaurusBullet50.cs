using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBullet50 : ScriptableEnemyBullet<TaurusBulletSystem52, Laser>
{
    const int LaserCount = 2;
    const float LaserSpacing = TaurusBulletSystem5.WaveSpacing;
    const float BulletRotationSpeed = 5f;

    protected override float MaxLifetime => 7f;

    protected override IEnumerator Move()
    {
        currentLifetime = 0f;
        yield return WaitForSeconds(0.5f);

        for (int i = 0; i < LaserCount; i++)
        {
            int d = i % 2 * 2 - 1;
            float r = d * (LaserSpacing / 8);
            float z = transform.eulerAngles.z + r;
            Vector3 pos = transform.position;

            var laser = SpawnBullet(0, z, pos, false);
            laser.Fire();
            laser.StartCoroutine(laser.RotateBy(d * 36f, 6f, delay: 0.5f));
        }
    }
}