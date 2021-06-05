using UnityEngine;

public class Player : Ship
{
    Vector2 velocity;

    protected override void Update()
    {
        velocity.x = Input.GetAxisRaw("Horizontal");
        velocity.y = Input.GetAxisRaw("Vertical");

        Move(velocity);
    }
}