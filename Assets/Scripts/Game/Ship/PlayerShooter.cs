using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class PlayerShooter : Shooter<PlayerBullet>
{
    [SerializeField] List<PlayerBullet> playerBullets = new List<PlayerBullet>();
    bool canShoot = true;    

    protected override void Awake()
    {
        base.Awake();

        PlayerBulletPool.Instance.UpdatePoolableObjects(playerBullets);
        ownerShip.LoseLifeAction += DestroyAllProjectiles;

        FindObjectOfType<PauseHandler>().GamePauseAction += OnGamePaused;
    }

    void Update()
    {
        GetShootingInput();
    }

    void GetShootingInput()
    {
        if (Input.GetButton("Shoot") && canShoot)
        {
            if (shootCoroutine != null) StopCoroutine(shootCoroutine);

            shootCoroutine = Shoot();
            StartCoroutine(shootCoroutine);
        }
    }

    protected override IEnumerator Shoot()
    {
        SpawnProjectile(0, 0f, transform.position, false);

        canShoot = false;
        yield return WaitForSeconds(ShootingCooldown);
        canShoot = true;
    }

    void OnGamePaused(bool state)
    {
        enabled = !state;
    }
}