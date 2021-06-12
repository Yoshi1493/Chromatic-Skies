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
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");

        Move(shipData.currentMovementSpeed.value);
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
        var newBullet = PlayerBulletPool.Instance.Get();

        newBullet.transform.SetPositionAndRotation(bulletSpawnPos.position, bulletSpawnPos.rotation);
        newBullet.gameObject.SetActive(true);
    }

    protected override void Die()
    {
        spriteRenderer.enabled = false;
        enabled = false;
    }
}