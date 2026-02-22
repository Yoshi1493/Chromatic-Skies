using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoShooter06 : CommonEnemyShooter<EnemyBullet>
{
    [SerializeField] LeoShooter05 parentShooter;

    const int WaveCount = 24;
    const int BranchCount = 12;
    const int BulletCount = 2;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletSpawnRadius = 0.5f;
    const float BulletRotationSpeed = 120f;
    const float BulletRotationSpeedModifier = -2f;

    protected override void Awake()
    {
        base.Awake();
        parentShooter.ShootAction += OnParentShooterStart;
    }

    void OnParentShooterStart()
    {
        if (shootCoroutine != null)
        {
            StopCoroutine(shootCoroutine);
        }

        shootCoroutine = Shoot();
        StartCoroutine(shootCoroutine);
    }

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1f);

        for (int i = 0; i < WaveCount; i++)
        {
            yield return WaitForSeconds(ShootingCooldown);

            for (int ii = 0; ii < BranchCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    int d = iii % 2 * 2 - 1;
                    float z = ii * BranchSpacing;
                    float r = d * (BulletRotationSpeed + (i * BulletRotationSpeedModifier));
                    Vector3 pos = BulletSpawnRadius * transform.up.RotateVectorBy(z);

                    var bullet = SpawnProjectile(6, z, pos);
                    bullet.StartCoroutine(bullet.RotateBy(r, 2f));
                    bullet.Fire();
                }
            }
        }
    }

    void OnDestroy()
    {
        parentShooter.ShootAction -= OnParentShooterStart;
    }
}