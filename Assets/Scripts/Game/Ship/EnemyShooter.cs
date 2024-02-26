using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public interface IEnemyAttack
{
    bool Enabled { get; }
    void SetEnabled(bool state);

    Action StartMoveAction { get; set; }
}

public abstract class EnemyShooter<TProjectile> : Shooter<TProjectile>, IEnemyAttack
    where TProjectile : Projectile
{
    #region Interface impl.

    bool IEnemyAttack.Enabled => enabled;
    void IEnemyAttack.SetEnabled(bool state) { enabled = state; }

    public Action StartMoveAction { get; set; }

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
        ownerShip.Invincible = false;
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

    protected override void OnLoseLife()
    {
        StopAllCoroutines();
        base.OnLoseLife();
    }

    void OnPlayerLoseLife()
    {
        StopAllCoroutines();
        DestroyAllProjectiles();
    }
}