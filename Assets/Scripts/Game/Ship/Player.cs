using UnityEngine;

public class Player : Ship
{
    [SerializeField] SpriteRenderer hitboxVisualizer;

    protected override void Awake()
    {
        base.Awake();
        shipData.Invincible = false;
    }

    protected override void Update()
    {
        base.Update();

        GetMovementInput();
        GetSlowInput();
    }

    void GetMovementInput()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");
    }

    protected override void Move(Vector3 direction, float speed)
    {
        //check for collision on world boundaries along x and y axes independently
        RaycastHit2D rayH = Physics2D.Raycast(transform.position, Vector3.right, 0.1f * moveDirection.x, shipData.boundaryLayer);
        RaycastHit2D rayV = Physics2D.Raycast(transform.position, Vector3.up, 0.1f * moveDirection.y, shipData.boundaryLayer);

        //if movement is restricted on one axis, still allow movement on the other axis
        if (rayH.collider != null)
            moveDirection.x = 0;

        if (rayV.collider != null)
            moveDirection.y = 0;

        base.Move(moveDirection.normalized, speed);
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