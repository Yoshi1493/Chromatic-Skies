using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static CoroutineHelper;

public class PlayerShooter : Shooter<Player, PlayerBullet>
{
    [SerializeField] List<Transform> bulletSpawnPositions = new();

    [SerializeField] InputActionAsset inputActions;
    InputAction fireInput;

    protected override void Start()
    {
        base.Start();

        parentShip.RespawnAction += OnRespawn;

        //manually enable to avoid script execution order conflicts
        GetComponent<PlayerSpecialShooter>().enabled = true;
    }

    void Update()
    {
        if (!PauseHandler.IsPaused)
        {
            GetShootingInput();
        }
    }

    void GetShootingInput()
    {
        if (fireInput.WasPressedThisFrame() && CanShoot)
        {
            if (shootCoroutine != null)
            {
                StopCoroutine(shootCoroutine);
            }

            shootCoroutine = Shoot();
            StartCoroutine(shootCoroutine);
        }
    }

    protected override IEnumerator Shoot()
    {
        SpawnProjectile(0, 0f, bulletSpawnPositions[0].position, false);
        //SpawnProjectile(0, 0f, bulletSpawnPositions[1].position, false);
        //SpawnProjectile(0, 0f, bulletSpawnPositions[2].position, false);

        CanShoot = false;
        yield return WaitForSeconds(ShootingCooldown);
        CanShoot = true;
    }

    protected override void OnLoseLife()
    {
        StopAllCoroutines();
        enabled = false;
    }

    void OnRespawn()
    {
        enabled = true;
    }
}