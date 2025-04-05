using UnityEngine;

public abstract class ShipMovement<TShip> : MonoBehaviour
    where TShip : Ship
{
    [HideInInspector] public TShip parentShip;

    protected virtual void Awake()
    {
        parentShip = GetComponentInParent<TShip>();
    }

    protected virtual void Start()
    {
        parentShip.LoseLifeAction += OnLoseLife;
        parentShip.DeathAction += OnDie;
    }

    protected virtual void Update()
    {
        if (parentShip.moveDirection != Vector3.zero && parentShip.MoveSpeed != 0f)
        {
            ApplyMovement();
        }
    }

    protected void ApplyMovement()
    {
        parentShip.transform.Translate(Time.deltaTime * parentShip.MoveSpeed * parentShip.moveDirection.normalized, Space.World);
    }

    protected abstract void OnLoseLife();

    void OnDie()
    {
        enabled = false;
    }
}