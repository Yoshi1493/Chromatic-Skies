using System;
using UnityEngine;

public abstract class Ship : Actor
{
    public ShipObject shipData;

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
        spriteRenderer.sprite = shipData.Sprite;

        //stats
        shipData.CurrentLives.Value = shipData.MaxLives.Value;

        shipData.CurrentHealth.Value= shipData.MaxHealth.Value;
        shipData.Power.Value = shipData.Power.Value;
        shipData.Defense.Value = shipData.Defense.Value;

        shipData.MovementSpeed.Value = shipData.MovementSpeed.Value;
        shipData.ShootingSpeed.Value = shipData.ShootingSpeed.Value;

        //debug
        name = shipData.ShipName.value;
    }

    //to-do: take shipData.Defense into account for damage calculations
    public void TakeDamage(int power)
    {
        shipData.CurrentHealth.Value -= power;
        //print($"{name} took {power} damage.");
        if (shipData.CurrentHealth.Value <= 0)
        {
            LoseLifeAction?.Invoke();
        }
    }

    protected virtual void LoseLife()
    {
        shipData.CurrentLives.Value--;

        if (shipData.CurrentLives.Value <= 0)
        {
            DeathAction?.Invoke();
            print($"{name} died.");
        }
        else
        {
            shipData.CurrentHealth.Value = shipData.MaxHealth.Value;
        }
    }

    protected abstract void Die();
}