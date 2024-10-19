using System;
using UnityEngine;

public class PlayerMovement : ShipMovement<Player>
{
    [SerializeField] FloatObject slowMovementSpeed;

    public event Action<bool> MovementSlowAction;

    PauseHandler pauseHandler;

    protected override void Awake()
    {
        base.Awake();
        pauseHandler = FindObjectOfType<PauseHandler>();
    }

    protected override void Start()
    {
        parentShip.RespawnAction += OnRespawn;
        MovementSlowAction += SetSlowState;
        pauseHandler.GamePauseAction += OnGamePaused;
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
        currentSpeed = state ? slowMovementSpeed.value : parentShip.shipData.MovementSpeed.Value;
    }

    protected override void OnLoseLife()
    {
        MovementSlowAction?.Invoke(false);
        enabled = false;
    }

    protected void OnRespawn()
    {
        enabled = true;
        MovementSlowAction?.Invoke(Input.GetButton("Slow"));
    }

    void OnGamePaused(bool state)
    {
        if (state)
        {
            MovementSlowAction?.Invoke(false);
        }
    }
}