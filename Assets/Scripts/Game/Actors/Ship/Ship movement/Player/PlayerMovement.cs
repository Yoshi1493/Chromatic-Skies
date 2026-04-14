using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static CameraBoundaries;

public class PlayerMovement : ShipMovement<Player>
{
    [SerializeField] ShipObject shipData;

    public event Action<bool> MovementSlowAction;
    [SerializeField] FloatObject slowMovementSpeed;

    PauseHandler pauseHandler;

    [SerializeField] InputActionAsset inputActions;
    InputAction moveInput;
    InputAction slowInput;

    protected override void Awake()
    {
        base.Awake();
        pauseHandler = FindAnyObjectByType<PauseHandler>();
        
        // get references to Input System's actions
        moveInput = inputActions.FindActionMap("Player").FindAction("Move");
        slowInput = inputActions.FindActionMap("Player").FindAction("Slow");
    }

    protected override void Start()
    {
        base.Start();

        parentShip.RespawnAction += OnRespawn;
        MovementSlowAction += SetSlowState;
        pauseHandler.GamePauseAction += OnGamePaused;

        parentShip.MoveSpeed = shipData.MovementSpeed.Value;
    }

    protected override void Update()
    {
        GetMovementInput();
        GetSlowInput();

        base.Update();
    }

    void GetMovementInput()
    {
        Vector2 moveInputValue = moveInput.ReadValue<Vector2>();

        parentShip.moveDirection.x = moveInputValue.x;
        parentShip.moveDirection.y = moveInputValue.y;

        //clamp player position to camera boundaries
        if (parentShip.transform.position.x < -ScreenHalfWidth)
        {
            parentShip.moveDirection.x = Mathf.Max(0, parentShip.moveDirection.x);
        }
        if (parentShip.transform.position.x > ScreenHalfWidth)
        {
            parentShip.moveDirection.x = Mathf.Min(parentShip.moveDirection.x, 0);
        }
        if (parentShip.transform.position.y < -ScreenHalfHeight)
        {
            parentShip.moveDirection.y = Mathf.Max(0, parentShip.moveDirection.y);
        }
        if (parentShip.transform.position.y > ScreenHalfHeight)
        {
            parentShip.moveDirection.y = Mathf.Min(parentShip.moveDirection.y, 0);
        }
    }

    void GetSlowInput()
    {
        if (slowInput.WasPressedThisFrame())
        {
            MovementSlowAction?.Invoke(true);
        }

        if (slowInput.WasReleasedThisFrame())
        {
            MovementSlowAction?.Invoke(false);
        }
    }

    void SetSlowState(bool state)
    {
        parentShip.MoveSpeed = state ? slowMovementSpeed.value : parentShip.shipData.MovementSpeed.Value;
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