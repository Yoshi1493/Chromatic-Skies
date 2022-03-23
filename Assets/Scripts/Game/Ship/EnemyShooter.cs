using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyAttack
{
    Action<StringObject, StringObject> AttackStartAction { get; set; }

    StringObject ModuleName { get; set; }
    StringObject AttackName { get; set; }

    bool Enabled { get; }
    void SetEnabled(bool state);
}

public abstract class EnemyShooter<TProjectile> : Shooter<TProjectile>, IEnemyAttack
    where TProjectile : Projectile
{
    #region Interface impl.

    public Action<StringObject, StringObject> AttackStartAction { get; set; }

    [field: SerializeField] public StringObject AttackName { get; set; }
    [field: SerializeField] public StringObject ModuleName { get; set; }

    bool IEnemyAttack.Enabled => enabled; 
    void IEnemyAttack.SetEnabled(bool state) { enabled = state; }

    #endregion

    Player playerShip;
    protected Vector3 PlayerPosition => playerShip.transform.position;

    [SerializeField] List<TProjectile> enemyProjectiles = new List<TProjectile>();

    protected void Start()
    {
        playerShip = FindObjectOfType<Player>();
        ownerShip.LoseLifeAction += OnLoseLife;
    }

    protected virtual void OnEnable()
    {
        GenericObjectPool<TProjectile>.Instance.UpdatePoolableObjects(enemyProjectiles);

        if (shootCoroutine != null)
            StopCoroutine(shootCoroutine);

        shootCoroutine = Shoot();
        StartCoroutine(shootCoroutine);
    }

    protected override IEnumerator Shoot()
    {
        yield return ownerShip.ReturnToOriginalPosition();

        yield return CoroutineHelper.WaitForSeconds(1f);

        AttackStartAction?.Invoke(ModuleName, AttackName);
        ownerShip.shipData.Invincible = false;
    }

    protected void SetSubsystemEnabled(int subsystemIndex)
    {
        if (transform.GetChild(subsystemIndex - 1).TryGetComponent(out IEnemyAttack subsystem))
        {
            if (!subsystem.Enabled)
            {
                subsystem.SetEnabled(true);
            }
            else
            {
                Debug.LogError("Error: Subsystem is already enabled.");
            }
        }
    }

    protected virtual void OnLoseLife()
    {
        StopCoroutine(shootCoroutine);
        GenericObjectPool<TProjectile>.Instance.DrainPool();
    }
}