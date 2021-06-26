using System;
using UnityEngine;

public abstract class Ship : Actor
{
    [SerializeField] protected ShipObject shipData;

    public event Action LoseLifeAction;
    public event Action DeathAction;

    protected override void Awake()
    {
        base.Awake();

        InitShip();

        LoseLifeAction += LoseLife;
        DeathAction += Die;
    }

    void InitShip()
    {
        //appearance
        spriteRenderer.sprite = shipData.sprite;

        //stats
        shipData.Lives.CurrentValue = shipData.Lives.OriginalValue;

        shipData.Health.CurrentValue = shipData.Health.OriginalValue;
        shipData.Power.CurrentValue = shipData.Power.OriginalValue;
        shipData.Defense.CurrentValue = shipData.Defense.OriginalValue;

        shipData.MovementSpeed.CurrentValue = shipData.MovementSpeed.OriginalValue;
        shipData.ShootingSpeed.CurrentValue = shipData.ShootingSpeed.OriginalValue;

        //debug
        name = shipData.shipName.value;
    }

    //to-do: take shipData.Defense into account for damage calculations
    public void TakeDamage(int power)
    {
        shipData.Health.CurrentValue -= power;

        print($"{name} took {power} damage.");

        if (shipData.Health.CurrentValue <= 0)
        {
            LoseLifeAction?.Invoke();
        }
    }

    protected virtual void LoseLife()
    {
        shipData.Lives.CurrentValue--;
        print($"{name} lost a life. currentLives: {shipData.Lives.CurrentValue}");

        shipData.Health.CurrentValue = shipData.Health.OriginalValue;

        if (shipData.Lives.CurrentValue <= 0)
        {
            DeathAction?.Invoke();
            print($"{name} died.");
        }
    }

    protected abstract void Die();
}