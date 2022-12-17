using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public interface IEnemyAttack
{
    Action<int> AttackStartAction { get; set; }

    bool Enabled { get; }
    void SetEnabled(bool state);
}

public abstract class EnemyShooter<TProjectile> : Shooter<TProjectile>, IEnemyAttack
    where TProjectile : Projectile
{
    #region Interface impl.

    public Action<int> AttackStartAction { get; set; }

    bool IEnemyAttack.Enabled => enabled;
    void IEnemyAttack.SetEnabled(bool state) { enabled = state; }

    #endregion

    Player playerShip;
    protected Vector3 PlayerPosition => playerShip.transform.position;

    protected float screenHalfHeight;
    protected float screenHalfWidth;

    [Space]

    [SerializeField] List<TProjectile> enemyProjectiles = new();
    [SerializeField] protected ProjectileObject bulletData;

    protected override void Awake()
    {
        base.Awake();

        //find player
        if (playerShip == null)
        {
            playerShip = FindObjectOfType<Player>();
        }

        //set screen dimensions
        Camera mainCam = Camera.main;
        screenHalfHeight = mainCam.orthographicSize;
        screenHalfWidth = screenHalfHeight * mainCam.aspect;
    }

    void Start()
    {
        //subscribe methods to actions
        ownerShip.LoseLifeAction += OnLoseLife;
        playerShip.LoseLifeAction += OnPlayerLoseLife;
    }

    protected virtual void OnEnable()
    {
        //update object pool
        GenericObjectPool<TProjectile>.Instance.UpdatePoolableObjects(enemyProjectiles);

        //start attack pattern
        if (shootCoroutine != null)
        {
            StopCoroutine(shootCoroutine);
        }

        shootCoroutine = Shoot();
        StartCoroutine(shootCoroutine);
    }

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1f);

        AttackStartAction?.Invoke(ownerShip.shipData.MaxLives.Value - ownerShip.currentLives);
        ownerShip.invincible = false;
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
        StopAllCoroutines();
    }

    void OnPlayerLoseLife()
    {
        StopAllCoroutines();
        DestroyAllProjectiles();
    }
}