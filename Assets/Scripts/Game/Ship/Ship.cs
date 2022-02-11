using System;

public abstract class Ship : Actor
{
    public ShipObject shipData;

    public event Action TakeDamageAction;
    public event Action LoseLifeAction;
    public event Action DeathAction;

    protected override void Awake()
    {
        base.Awake();

        InitShipData();

        LoseLifeAction += LoseLife;
        DeathAction += Die;
    }

    void InitShipData()
    {
        //appearance
        spriteRenderer.sprite = shipData.Sprite;

        //stats
        shipData.CurrentLives.Value = shipData.MaxLives.Value;
        shipData.CurrentHealth.Value = shipData.MaxHealth.Value;
        shipData.CurrentSpeed = shipData.MovementSpeed.Value;

        //debug
        name = shipData.ShipName.value;
    }

    protected virtual void Update()
    {
        Move(moveDirection.normalized, shipData.CurrentSpeed);
    }

    //to-do: take shipData.Defense into account for damage calculations
    public void TakeDamage(int power)
    {
        shipData.CurrentHealth.Value -= power;
        //print($"{name} took {power} damage.");

        if (shipData.CurrentHealth.Value <= 0)
            LoseLifeAction?.Invoke();

        else
            TakeDamageAction?.Invoke();

    }

    protected virtual void LoseLife()
    {
        shipData.CurrentLives.Value--;

        if (shipData.CurrentLives.Value <= 0)
        {
            DeathAction?.Invoke();
        }
        else
        {
            shipData.CurrentHealth.Value = shipData.MaxHealth.Value;
            shipData.Invincible = true;
        }
    }

    protected abstract void Die();
}