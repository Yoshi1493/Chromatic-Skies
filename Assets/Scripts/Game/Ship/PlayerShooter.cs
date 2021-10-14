using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class PlayerShooter : Shooter
{
    [SerializeField] List<PlayerBullet> playerBullets = new List<PlayerBullet>();
    bool canShoot = true;    

    protected override void Awake()
    {
        base.Awake();

        PlayerBulletPool.Instance.UpdatePoolableObjects(playerBullets);
        ownerShip.LoseLifeAction += DestroyAllProjectiles<PlayerBullet>;

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
        SpawnBullet(transform.up);

        canShoot = false;
        yield return WaitForSeconds(ShootingCooldown);
        canShoot = true;
    }

    void SpawnBullet(Vector3 offset)
    {
        var newBullet = PlayerBulletPool.Instance.Get(0);

        newBullet.transform.SetPositionAndRotation(ShipPosition + offset, Quaternion.identity);
        newBullet.gameObject.SetActive(true);
        newBullet.enabled = true;
    }

    void OnGamePaused(bool state)
    {
        enabled = !state;
    }
}