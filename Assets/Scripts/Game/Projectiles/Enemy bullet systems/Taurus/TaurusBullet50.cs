using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBullet50 : ScriptableEnemyBullet<TaurusBulletSystem52, Laser>
{
    const int RepeatCount = 2;
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
            for (int i = 0; i < RepeatCount; i++)
            {
                int d = i % 2 * 2 - 1;
                yield return WaitForSeconds(ShootingCooldown);

                for (int ii = 0; ii < LaserCount; ii++)
                {
                    int t = ii % 2 * 2 - 1;
                    float r = t * (LaserSpacing / 8);
                    float z = transform.eulerAngles.z + r;
                    Vector3 pos = transform.position;

                    var laser = SpawnBullet(0, z, pos, false);
                    laser.Fire(1f);
                    laser.StartCoroutine(laser.RotateBy(t * 36f, 6f, delay: 1f));
                }

                yield return WaitForSeconds(7f);
                StartCoroutine(this.RotateBy(180f, 1f));
                yield return this.TransformRotateAround(rotationOrigin, 1f, 180f);
                yield return WaitForSeconds(ShootingCooldown);

                for (int ii = 0; ii < LaserCount; ii++)
                {
                    int t = ii % 2 * 2 - 1;
                    float r = t * (LaserSpacing / 8);
                    float z = transform.eulerAngles.z + r;
                    Vector3 pos = transform.position;

                    var laser = SpawnBullet(0, z, pos, false);
                    laser.Fire(1f);
                    laser.StartCoroutine(laser.RotateBy(d * 36f, 6f, delay: 1f));
                }

                yield return WaitForSeconds(7f);
                StartCoroutine(this.RotateBy(180f, 1f));
                yield return this.TransformRotateAround(rotationOrigin, 1f, 180f);
            }
        }

    }
}