using System;
using UnityEngine;

public class PlayerMovement : ShipMovement
{
    public event Action<bool> MovementSlowAction;

    protected override void Awake()
    {
        base.Awake();

        parentShip.RespawnAction += OnRespawn;
        MovementSlowAction += SetSlowState;

        FindObjectOfType<PauseHandler>().GamePauseAction += OnGamePaused;
    }

    protected override void Update()
    {
        GetMovementInput();
        GetSlowInput();

        base.Update();
    }

    void GetMovementInput()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");

        //check for collision on world boundaries along x and y axes independently
        RaycastHit2D rayH = Physics2D.Raycast(transform.position, Vector3.right, 0.1f * moveDirection.x, shipData.boundaryLayer);
        RaycastHit2D rayV = Physics2D.Raycast(transform.position, Vector3.up, 0.1f * moveDirection.y, shipData.boundaryLayer);

        //if movement is restricted on one axis, still allow movement on the other axis
        if (rayH.collider != null)
            moveDirection.x = 0;

        if (rayV.collider != null)
            moveDirection.y = 0;
    }

    void GetSlowInput()
    {
        if (Input.GetButtonDown("Slow"))
        {
            MovementSlowAction?.Invoke(true);
        }

        if (Input.GetButtonUp("Slow"))
        {
            MovementSlowAction?.Invoke(false);
        }
    }

    void SetSlowState(bool state)
    {
        currentSpeed = parentShip.shipData.MovementSpeed.Value * (state ? 0.5f : 1);
    }

    protected override void OnLoseLife()
    {
        currentSpeed = 0f;
    }

    protected void OnRespawn()
    {
        currentSpeed = shipData.MovementSpeed.Value;
    }

    void OnGamePaused(bool state)
    {
        if (state)
        {
            MovementSlowAction?.Invoke(false);
        }
    }
}