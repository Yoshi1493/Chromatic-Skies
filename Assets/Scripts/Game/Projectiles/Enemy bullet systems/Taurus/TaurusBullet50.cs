using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBullet50 : ScriptableEnemyBullet<TaurusBulletSystem52, Laser>
{
    const int LaserCount = 2;
    const float LaserSpacing = TaurusBulletSystem5.WaveSpacing;
    const float BulletRotationSpeed = 5f;
    const float ShootingCooldown = 0.5f;
    Vector3 rotationOrigin = 2.5f * Vector3.up;

    protected override float MaxLifetime => Mathf.Infinity;

    protected override IEnumerator Move()
    {
        while (enabled)
        {
            yield return WaitForSeconds(ShootingCooldown);

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

            yield return WaitForSeconds(6.5f);
            StartCoroutine(this.RotateBy(180f, 1f));
            yield return this.TransformRotateAround(rotationOrigin, 1f, 180f);
            yield return WaitForSeconds(ShootingCooldown);

            for (int i = 0; i < LaserCount; i++)
            {
                int d = i % 2 * 2 - 1;
                float r = d * (LaserSpacing / 8) + 180f;
                float z = transform.eulerAngles.z + r;
                Vector3 pos = transform.position;

                SpawnBullet(1, z, pos, false).Fire();
            }

            yield return WaitForSeconds(6.5f);
            StartCoroutine(this.RotateBy(180f, 1f));
            yield return this.TransformRotateAround(rotationOrigin, 1f, 180f);
        }
    }
}