public abstract class Ship : Actor
{
    protected ShipObject shipData;

    public delegate void DeathAction();
    public DeathAction deathAction;

    protected override void Awake()
    {
        base.Awake();

        InitShip();
        deathAction += Die;
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
            LoseLife();
        }
    }

    protected virtual void LoseLife()
    {
        shipData.Lives.CurrentValue--;

        if (shipData.Lives.CurrentValue <= 0)
        {
            deathAction?.Invoke();
        }
    }

    protected abstract void Die();
}