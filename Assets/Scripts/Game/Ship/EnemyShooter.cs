using System;
using System.Collections;
using UnityEngine;

public interface INamedAttack
{
    Action<StringObject, StringObject> AttackStartAction { get; set; }

    StringObject ModuleName { get; set; }
    StringObject AttackName { get; set; }

    void SetEnabled(bool state);
}

public abstract class EnemyShooter<TProjectile> : Shooter<TProjectile>, INamedAttack
    where TProjectile : Projectile
{
    Player playerShip;
    protected Vector2 PlayerPosition => playerShip.transform.position;

    [field: SerializeField] public StringObject ModuleName { get; set; }
    [field: SerializeField] public StringObject AttackName { get; set; }
    public Action<StringObject, StringObject> AttackStartAction { get; set; }

    void INamedAttack.SetEnabled(bool state)
    {
        enabled = state;
    }

    protected virtual void Start()
    {
        playerShip = FindObjectOfType<Player>();
        ownerShip.LoseLifeAction += OnLoseLife;
    }

    protected virtual void OnEnable()
    {
        if (shootCoroutine != null) StopCoroutine(shootCoroutine);
        shootCoroutine = Shoot();
        StartCoroutine(shootCoroutine);
    }

    protected override IEnumerator Shoot()
    {
        yield return CoroutineHelper.WaitForSeconds(1f);
        AttackStartAction?.Invoke(ModuleName, AttackName);
    }

    protected void SetSubsystemEnabled(int subsystemIndex)
    {
        if (transform.GetChild(subsystemIndex - 1).TryGetComponent(out INamedAttack subsystem))
            subsystem.SetEnabled(true);
    }

    protected virtual void OnLoseLife()
    {
        StopCoroutine(shootCoroutine);
        DestroyAllProjectiles();
    }
}