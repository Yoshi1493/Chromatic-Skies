using System.Collections;
using UnityEngine;

public class CancerBulletSystem31 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 16;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletSpawnRadius = 4f;

    float z;
    float smoothDampVel = 0f;
    const float SmoothTime = 1.2f;

    protected override IEnumerator Shoot()
    {
        yield return null;

        for (int i = 0; i < BulletCount; i++)
        {
            int b = i == 0 ? 0 : i % 4 == 0 ? 1 : 2;
            float z = i * BulletSpacing;
            Vector3 pos = Vector3.zero;

            SpawnProjectile(b + 3, z, pos).Fire();
        }
    }

    void Update()
    {
        UpdatePosition();
    }

    void UpdatePosition()
    {
        float r = PlayerPosition.GetRotationDifference(transform.position);
        z = Mathf.SmoothDamp(z, r, ref smoothDampVel, SmoothTime);

        transform.eulerAngles = z * Vector3.forward;
    }
}