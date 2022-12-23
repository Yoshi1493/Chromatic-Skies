using UnityEngine;

public abstract class ShipMovement : MonoBehaviour
{
    public ShipObject shipData;
    [HideInInspector] public Ship parentShip;

    [HideInInspector] public Vector3 moveDirection;
    [HideInInspector] public float currentSpeed;

    protected virtual void Awake()
    {
        parentShip = GetComponentInParent<Ship>();
        parentShip.LoseLifeAction += OnLoseLife;

        FindObjectOfType<PauseHandler>().GamePauseAction += OnGamePaused;

        currentSpeed = shipData.MovementSpeed.Value;
    }

    protected virtual void Update()
    {
        ApplyMovement();
    }

    protected void ApplyMovement()
    {
        parentShip.transform.Translate(Time.deltaTime * currentSpeed * moveDirection.normalized, Space.World);
    }

    protected abstract void OnLoseLife();

    protected virtual void OnGamePaused(bool state)
    {
        enabled = !state;
    }
}