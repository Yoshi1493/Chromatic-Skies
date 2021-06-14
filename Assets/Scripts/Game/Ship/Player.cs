using UnityEngine;

public class Player : Ship
{
    void Update()
    {
        GetMovementInput();
    }

    void GetMovementInput()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");

        Move(shipData.currentMovementSpeed.value);
    }

    protected override void Die()
    {
        spriteRenderer.enabled = false;
        enabled = false;
    }
}