using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class Enemy : Ship
{
    [SerializeField] Transform bulletSystemContainer;
    [SerializeField] Transform movementSystemContainer;

    public List<IEnemyAttack> bulletSystems { get; private set; }
    List<EnemyMovement> movementSystems;

    public event Action<int> StartAttackAction;
    IEnumerator systemResetCoroutine;

    Player player;

    protected override void Awake()
    {
        base.Awake();
        SpriteRenderer.color = shipData.UIColour.value;                 //debug

        ValidateAttackSystems();

        player = FindObjectOfType<Player>();
        player.LoseLifeAction += OnPlayerLoseLife;
    }

    void ValidateAttackSystems()
    {
        bulletSystems = new(shipData.MaxLives.Value);
        movementSystems = new(movementSystemContainer.childCount);

        for (int i = 0; i < bulletSystemContainer.childCount; i++)
        {
            if (bulletSystemContainer.GetChild(i).TryGetComponent(out IEnemyAttack enemyAttack))
            {
                bulletSystems.Add(enemyAttack);
            }
        }

        for (int i = 0; i < movementSystemContainer.childCount; i++)
        {
            if (movementSystemContainer.GetChild(i).TryGetComponent(out EnemyMovement enemyMovement))
            {
                movementSystems.Add(enemyMovement);
            }
        }
    }

    void OnEnable()
    {
        SetInvincible(3f);

        if (systemResetCoroutine != null)
        {
            StopCoroutine(systemResetCoroutine);
        }

        systemResetCoroutine = RefreshEnemySystems(0, RespawnTime);
        StartCoroutine(systemResetCoroutine);
    }

#if UNITY_EDITOR
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            TakeDamage(currentHealth);
    }
#endif

    //disable current systems, and enable next systems upon losing life
    protected override IEnumerator LoseLife()
    {
        int currentSystemIndex = shipData.MaxLives.Value - currentLives;

        StartCoroutine(base.LoseLife());

        EnemyBulletPool.Instance.DrainPool();
        EnemyLaserPool.Instance.DrainPool();

        if (currentLives > 0)
        {
            if (systemResetCoroutine != null)
            {
                StopCoroutine(systemResetCoroutine);
            }

            systemResetCoroutine = RefreshEnemySystems(currentSystemIndex, RespawnTime);
            yield return systemResetCoroutine;
        }
    }

    //disable and re-enable current systems upon player losing life
    void OnPlayerLoseLife()
    {
        int currentSystemIndex = shipData.MaxLives.Value - currentLives;

        SetInvincible(player.RespawnTime);

        if (systemResetCoroutine != null)
        {
            StopCoroutine(systemResetCoroutine);
        }

        systemResetCoroutine = RefreshEnemySystems(currentSystemIndex, player.RespawnTime);
        StartCoroutine(systemResetCoroutine);
    }

    IEnumerator RefreshEnemySystems(int currentSystemIndex, float refreshTime)
    {
        List<IEnemyAttack> currentBulletSystems = GetCurrentBulletSystem();
        EnemyMovement currentMovementSystem = GetCurrentMovementSystem();

        IEnemyAttack nextBulletSystem;
        EnemyMovement nextMovementSystem;

        if (currentHealth > 0)
        {
            nextBulletSystem = currentBulletSystems[0];
            nextMovementSystem = currentMovementSystem;
        }
        else
        {
            nextBulletSystem = bulletSystems[currentSystemIndex + 1];
            nextMovementSystem = movementSystems[currentSystemIndex + 1];
        }

        foreach (var bulletSystem in currentBulletSystems)
        {
            bulletSystem.SetEnabled(false);
        }

        currentMovementSystem.StopAllCoroutines();
        currentMovementSystem.enabled = false;

        nextMovementSystem.enabled = true;

        yield return WaitForSeconds(refreshTime);

        nextBulletSystem.SetEnabled(true);

        StartAttackAction?.Invoke(currentSystemIndex);
    }

    public List<IEnemyAttack> GetCurrentBulletSystem()
    {
        List<IEnemyAttack> currentBulletSystems = new();

        for (int i = 0; i < bulletSystemContainer.childCount; i++)
        {
            Transform child = bulletSystemContainer.GetChild(i);

            if (child.TryGetComponent(out IEnemyAttack bulletSystem) && bulletSystem.Enabled)
            {
                currentBulletSystems.Add(bulletSystem);

                if (child.childCount > 0)
                {
                    for (int ii = 0; ii < child.childCount; ii++)
                    {
                        if (child.GetChild(ii).TryGetComponent(out IEnemyAttack bulletSubsystem) && bulletSubsystem.Enabled)
                        {
                            currentBulletSystems.Add(bulletSubsystem);
                        }
                    }
                }

                break;
            }
        }

        return currentBulletSystems;
    }

    public EnemyMovement GetCurrentMovementSystem()
    {
        for (int i = 0; i < movementSystems.Count; i++)
        {
            if (movementSystems[i].enabled)
            {
                return movementSystems[i];
            }
        }

        //if no movement system is active (somehow)
        return movementSystems[shipData.MaxLives.Value - currentLives];
    }

    protected override IEnumerator Die()
    {
        yield return base.Die();
        enabled = false;
    }
}