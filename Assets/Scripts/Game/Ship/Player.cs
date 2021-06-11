using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class Player : Ship
{
    [SerializeField] protected Transform bulletSpawnPos;

    bool canShoot = true;
    float ShootCooldown => 1 / shipData.currentShootingSpeed.value;

    void Update()
    {
        GetMovementInput();
        GetShootingInput();
    }

    void GetMovementInput()
    {
        velocity.x = Input.GetAxisRaw("Horizontal");
        velocity.y = Input.GetAxisRaw("Vertical");

        Move(velocity);
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
        SpawnBullet(shipData.defaultBullet);
        canShoot = false;

        yield return WaitForSeconds(ShootCooldown);

        canShoot = true;
    }

    protected override void SpawnBullet(GameObject bullet)
    {
        Instantiate(bullet, bulletSpawnPos.position, bulletSpawnPos.rotation);
    }

    protected override void Die()
    {

    }
}