using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoShooter08 : CommonEnemyShooter<Laser>
{
    [SerializeField] LeoShooter05 parentShooter;

    const int WaveCount = 3;
    const int LaserCount = 10;

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
        yield return WaitForSeconds(3.5f);

        for (int i = 0; i < LaserCount; i++)
        {
            float z = transform.position.GetRotationDifference(PlayerPosition);
            Vector3 pos = Vector3.zero;

            SpawnProjectile(0, z, pos).Fire(1f);
            yield return WaitForSeconds(ShootingCooldown);
        }
    }

    void OnDestroy()
    {
        parentShooter.ShootAction -= OnParentShooterStart;
    }
}