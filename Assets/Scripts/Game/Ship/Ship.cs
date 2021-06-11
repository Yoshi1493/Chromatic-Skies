using UnityEngine;

public abstract class Ship : Actor
{
    [SerializeField] protected ShipObject shipData;    

    public event System.Action DeathAction;

    protected override void Awake()
    {
        base.Awake();

        InitShip();
        DeathAction += Die;
    }

    void InitShip()
    {
        //appearance
        spriteRenderer.sprite = shipData.sprite;

        //stats
        shipData.currentHealth.value = shipData.maxHealth;
        shipData.currentPower.value = shipData.originalPower;
        shipData.currentDefense.value = shipData.originalDefense;
        shipData.currentMovementSpeed.value = shipData.originalMovementSpeed;
        shipData.currentShootingSpeed.value = shipData.originalShootingSpeed;

        name = shipData.shipName.value;     //debug
    }

    protected abstract void SpawnBullet(GameObject bullet);

    protected void TakeDamage(int amount)
    {
        shipData.currentHealth.value -= amount;

        if (shipData.currentHealth.value <= 0)
        {
            DeathAction?.Invoke();
        }
    }

    protected abstract void Die();
}