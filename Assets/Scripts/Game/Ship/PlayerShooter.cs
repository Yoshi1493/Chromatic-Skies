using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class PlayerShooter : Shooter
{
    [SerializeField] List<PlayerBullet> playerBullets = new List<PlayerBullet>();
    bool canShoot = true;

    IEnumerator shootCoroutine;

    protected override void Awake()
    {
        base.Awake();

        PlayerBulletPool.Instance.UpdatePoolableBullets(playerBullets);
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

    IEnumerator Shoot()
    {
        SpawnBullet(0, 0);

        canShoot = false;
        yield return WaitForSeconds(ShootingCooldown);
        canShoot = true;
    }

    protected override void SpawnBullet(int bulletIndex, int spawnPositionIndex)
    {
        if (spawnPositionIndex >= spawnPositions.Count) return;

        var newBullet = PlayerBulletPool.Instance.Get(bulletIndex);

        newBullet.transform.SetPositionAndRotation(spawnPositions[spawnPositionIndex].position, spawnPositions[spawnPositionIndex].rotation);
        newBullet.gameObject.SetActive(true);
    }
}