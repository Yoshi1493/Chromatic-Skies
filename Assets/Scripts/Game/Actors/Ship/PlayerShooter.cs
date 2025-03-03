using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class PlayerShooter : Shooter<PlayerBullet>
{
    ShipObject parentShip;
    protected override float ShootingCooldown => 1 / parentShip.ShootingSpeed.Value;

    [SerializeField] List<Transform> bulletSpawnPositions = new();
    protected bool canShoot = true;

    protected override void Awake()
    {
        base.Awake();
        parentShip = GetComponentInParent<Player>().shipData;
    }

    protected override void Start()
    {
        base.Start();

        ownerShip.RespawnAction += OnRespawn;

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
        if (Input.GetButton("Shoot") && canShoot)
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

        AudioManager.Instance.PlayAudio("player_shoot-default", AudioType.Sound, true, 3);

        canShoot = false;
        yield return WaitForSeconds(ShootingCooldown);
        canShoot = true;
    }

    protected override void OnLoseLife()
    {
        base.OnLoseLife();
        enabled = false;
    }

    void OnRespawn()
    {
        enabled = true;
    }

    protected override void DestroyAllProjectiles()
    {
        foreach (var projectile in PlayerBulletPool.Instance.transform.GetComponentsInChildren<Projectile>())
        {
            if (projectile.isActiveAndEnabled)
            {
                projectile.Destroy();

                Vector3 pos = projectile.transform.position;
                projectile.SpawnDestructionParticles(pos);
            }
        }
    }
}