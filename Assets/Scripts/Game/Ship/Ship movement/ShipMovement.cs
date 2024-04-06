using UnityEngine;

public abstract class ShipMovement<TShip> : MonoBehaviour
    where TShip : Ship
{
    public ShipObject shipData;
    [HideInInspector] public TShip parentShip;

    [HideInInspector] public Vector3 moveDirection;
    [HideInInspector] public float currentSpeed;

    protected virtual void Awake()
    {
        parentShip = GetComponentInParent<TShip>();
        parentShip.LoseLifeAction += OnLoseLife;
        parentShip.DeathAction += OnDie;

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

    void OnDie()
    {
        enabled = false;
    }
}