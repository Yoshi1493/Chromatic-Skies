using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class PlayerShooter : Shooter
{
    protected List<Transform> spawnPositions = new List<Transform>();

    bool canShoot = true;

    void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            spawnPositions.Add(transform.GetChild(i));
        }
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