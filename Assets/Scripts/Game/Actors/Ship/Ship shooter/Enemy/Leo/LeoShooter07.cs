using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoShooter07 : CommonEnemyShooter<EnemyBullet>
{
    [SerializeField] LeoShooter05 parentShooter;

    const int WaveCount = 40;
    const int BulletCount = 6;
    const float ArcHalfWidth = 75;

    protected override float ShootingCooldown => 0.05f;

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
        yield return WaitForSeconds(6.5f);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = Random.Range(-ArcHalfWidth, ArcHalfWidth) + 180f;
                Vector3 pos = Vector3.zero;

                SpawnProjectile(7, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }

    void OnDestroy()
    {
        parentShooter.ShootAction -= OnParentShooterStart;
    }
}