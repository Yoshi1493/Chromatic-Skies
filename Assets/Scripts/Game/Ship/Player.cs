using UnityEngine;

public class Player : Ship
{
    Vector2 velocity;

    protected override void Update()
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
        if (Input.GetButton("Shoot"))
        {
            Shoot(shipData.defaultBullet);
        }
    }

    protected override void Shoot(GameObject bullet)
    {
        GameObject newBullet = Instantiate(bullet, bulletSpawnPos.position, bulletSpawnPos.rotation);
    }
}