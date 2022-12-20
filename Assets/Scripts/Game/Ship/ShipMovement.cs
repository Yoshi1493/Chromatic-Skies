using UnityEngine;

public abstract class ShipMovement : MonoBehaviour
{
    protected Ship parentShip;
    public ShipObject shipData;

    [HideInInspector] public Vector3 moveDirection;
    [HideInInspector] public float currentSpeed;

    protected virtual void Awake()
    {
        parentShip = GetComponentInParent<Ship>();
        parentShip.LoseLifeAction += OnLoseLife;
        parentShip.RespawnAction += OnRespawn;

        FindObjectOfType<PauseHandler>().GamePauseAction += OnGamePaused;

        currentSpeed = shipData.MovementSpeed.Value;
    }

    protected abstract void Move();

    protected abstract void OnLoseLife();

    protected abstract void OnRespawn();

    protected virtual void OnGamePaused(bool state)
    {
        enabled = !state;
    }
}