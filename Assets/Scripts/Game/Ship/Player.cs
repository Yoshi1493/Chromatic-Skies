using UnityEngine;

public class Player : Ship
{
    [SerializeField] GameObject hitboxVisualizer;

    protected override void Awake()
    {
        base.Awake();
        shipData.CurrentSpeed = shipData.MovementSpeed.Value;
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

    void GetSlowInput()
    {
        if (Input.GetButtonDown("Slow"))
        {
            hitboxVisualizer.SetActive(true);
            shipData.CurrentSpeed = shipData.MovementSpeed.Value / 2f;
        }

        if (Input.GetButtonUp("Slow"))
        {
            hitboxVisualizer.SetActive(false);
            shipData.CurrentSpeed = shipData.MovementSpeed.Value;
        }
    }

    protected override void Die()
    {
        spriteRenderer.enabled = false;
        enabled = false;
    }
}