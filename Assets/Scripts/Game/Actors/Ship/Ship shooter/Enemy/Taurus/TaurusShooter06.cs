using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;
using static CameraBoundaries;

public class TaurusShooter06 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 6;
    const int BranchCount = TaurusShooter05.BranchCount;
    const float BranchSpacing = 360f / BranchCount;
    const float ArcHalfWidth = 60f;
    const int BulletCount = 2;
    const float BulletSpawnOffset = -TaurusShooter05.LaserSpawnOffset;
    const float BulletSpawnRadius = TaurusShooter05.LaserSpawnRadius;

    [SerializeField] TaurusShooter05 parentShooter;
    [SerializeField] LayerMask bulletBoundaryLayer;

    List<(Vector3 pos, float z)> bulletSpawnData = new(BranchCount);

    protected override void Awake()
    {
        base.Awake();
        parentShooter.ShootAction += OnParentShooterShoot;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        StopCoroutine(shootCoroutine);
    }

    void OnParentShooterShoot(float angle)
    {
        bulletSpawnData.Clear();

        float rayDistance = 2f * new Vector2(ScreenHalfWidth, ScreenHalfHeight).magnitude;

        for (int i = 0; i < bulletSpawnData.Capacity; i++)
        {
            Vector2 rayOrigin = transform.position + (BulletSpawnRadius * transform.up.RotateVectorBy((i * BranchSpacing) + BulletSpawnOffset));
            Vector2 rayDirection = transform.up.RotateVectorBy((i * BranchSpacing) + angle);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance, bulletBoundaryLayer);

            if (hit)
            {
                Vector2 normal = hit.normal;
                float z = Mathf.Atan2(normal.x, -normal.y) * Mathf.Rad2Deg;
                bulletSpawnData.Add((hit.point, z));
            }
            else
            {
                bulletSpawnData.Add((Vector3.zero, 0f));
            }
        }

        StartCoroutine(Shoot());
    }

    protected override IEnumerator Shoot()
    {
        var currentBulletSpawnData = new List<(Vector3 pos, float z)>(bulletSpawnData);

        yield return WaitForSeconds(0.5f);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float z = currentBulletSpawnData[ii].z + Random.Range(-ArcHalfWidth, ArcHalfWidth);
                    Vector3 pos = currentBulletSpawnData[ii].pos;

                    SpawnProjectile(4, z, pos, false).Fire();
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }

    void OnDestroy()
    {
        parentShooter.ShootAction -= OnParentShooterShoot;
    }
}