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
            Run.Coroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        SpawnBullet(0);

        canShoot = false;
        yield return WaitForSeconds(ShootingCooldown);
        canShoot = true;
    }

    protected override void SpawnBullet(int index)
    {
        var newBullet = PlayerBulletPool.Instance.Get(index);

        newBullet.transform.SetPositionAndRotation(spawnPositions[0].position, spawnPositions[0].rotation);
        newBullet.gameObject.SetActive(true);
    }
}