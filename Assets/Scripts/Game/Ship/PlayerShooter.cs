using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class PlayerShooter : Shooter<PlayerBullet>
{
    [SerializeField] protected FloatObject shootingSpeed;
    protected override float ShootingCooldown => 1 / shootingSpeed.value;

    [SerializeField] List<PlayerBullet> playerBullets = new List<PlayerBullet>();
    bool canShoot = true;

    [SerializeField] List<Transform> bulletSpawnPositions = new List<Transform>();

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
        SpawnProjectile(0, 0f, bulletSpawnPositions[0].position, false);
        //SpawnProjectile(0, 0f, bulletSpawnPositions[1].position, false);
        //SpawnProjectile(0, 0f, bulletSpawnPositions[2].position, false);

        canShoot = false;
        yield return WaitForSeconds(ShootingCooldown);
        canShoot = true;
    }

    void OnGamePaused(bool state)
    {
        enabled = !state;
    }
}