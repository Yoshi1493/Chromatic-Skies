using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class PlayerShooter : Shooter<PlayerBullet>
{
    ShipObject shipData;
    protected override float ShootingCooldown => 1 / shipData.ShootingSpeed.Value;

    [SerializeField] List<Transform> bulletSpawnPositions = new();
    public bool CanShoot { get; protected set; }

    protected override void Awake()
    {
        base.Awake();
        shipData = GetComponentInParent<Player>().shipData;
    }

    protected override void Start()
    {
        base.Start();

        (parentShip as CharacterShip).RespawnAction += OnRespawn;
        CanShoot = true;

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
        if (Input.GetButton("Shoot") && CanShoot)
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
        base.OnLoseLife();
        enabled = false;
    }

    void OnRespawn()
    {
        enabled = true;
    }

    protected override void DestroyAllProjectiles()
    {
        foreach (var bullet in PlayerBulletPool.Instance.GetAllActiveObjects())
        {
            Vector3 pos = bullet.transform.position;

            bullet.Destroy();
            bullet.SpawnDestructionParticles(pos);
        }
    }
}