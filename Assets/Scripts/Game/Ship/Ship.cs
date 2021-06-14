public abstract class Ship : Actor
{
    public ShipObject shipData;

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
        shipData.Health.CurrentValue = shipData.Health.OriginalValue;
        shipData.Power.CurrentValue = shipData.Power.OriginalValue;
        shipData.Defense.CurrentValue = shipData.Defense.OriginalValue;
        shipData.MovementSpeed.CurrentValue = shipData.MovementSpeed.OriginalValue;
        shipData.ShootingSpeed.CurrentValue = shipData.ShootingSpeed.OriginalValue;

        //debug
        name = shipData.shipName.value;
    }

    protected void TakeDamage(int amount)
    {
        shipData.Health.CurrentValue -= amount;

        if (shipData.Health.CurrentValue <= 0)
        {
            deathAction?.Invoke();
        }
    }

    protected abstract void Die();
}