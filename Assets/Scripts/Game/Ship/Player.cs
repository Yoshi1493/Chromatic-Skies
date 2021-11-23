using UnityEngine;

public class Player : Ship
{
    [SerializeField] SpriteRenderer hitboxVisualizer;

    protected override void Awake()
    {
        base.Awake();

        shipData.CurrentSpeed = shipData.MovementSpeed.Value;
        shipData.Invincible = false;
    }

    void Update()
    {
        GetMovementInput();
        GetSlowInput();
    }

    void GetMovementInput()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");

        Move(shipData.CurrentSpeed);
    }

    protected override void Move(float moveSpeed)
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, moveDirection, 0.1f, shipData.boundaryLayer);

        if (ray.collider == null)
            base.Move(moveSpeed);
    }

    void GetSlowInput()
    {
        if (Input.GetButtonDown("Slow"))
        {
            hitboxVisualizer.enabled = true;
            shipData.CurrentSpeed = shipData.MovementSpeed.Value / 2f;
        }

        if (Input.GetButtonUp("Slow"))
        {
            hitboxVisualizer.enabled = false;
            shipData.CurrentSpeed = shipData.MovementSpeed.Value;
        }
    }

    protected override void Die()
    {
        spriteRenderer.enabled = false;
        enabled = false;
    }
}