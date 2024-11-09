using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PlayerSpecialShooter : Shooter<PlayerBullet>
{
    [SerializeField] FloatObject specialMeter;
    bool canShoot = true;

    protected override float ShootingCooldown => 5f;

    protected override void Start()
    {
        base.Start();
        specialMeter.value = 100;
    }

    void Update()
    {
        GetShootingInput();
    }

    void GetShootingInput()
    {
        if (Input.GetButtonDown("Special"))
        {
            if (canShoot && specialMeter.value >= 100)
            {
                if (shootCoroutine != null)
                {
                    StopCoroutine(shootCoroutine);
                }

                shootCoroutine = Shoot();
                StartCoroutine(shootCoroutine);
            }
        }
    }

    protected override IEnumerator Shoot()
    {
        SpawnProjectile(0, 0f, transform.position, false);

        canShoot = false;
        yield return WaitForSeconds(ShootingCooldown);

        canShoot = true;
    }
}