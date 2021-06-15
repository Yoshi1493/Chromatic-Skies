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

    public void TakeDamage(int power, int defense)
    {
        shipData.Health.CurrentValue -= power;

        print($"{name} took {power} damage.");

        if (shipData.Health.CurrentValue <= 0)
        {
            deathAction?.Invoke();
        }
    }

    protected abstract void Die();
}