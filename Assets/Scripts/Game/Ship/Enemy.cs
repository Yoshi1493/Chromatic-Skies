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
    List<IEnemyAttack> currentBulletSystems = new();
    IEnemyAttack nextBulletSystem;

    List<EnemyMovement> movementSystems;
    EnemyMovement currentMovementSystem;
    EnemyMovement nextMovementSystem;

    public event Action<int> StartAttackAction;
    IEnumerator systemResetCoroutine;

    protected override void Awake()
    {
        base.Awake();

        ValidateAttackSystems();
        FindObjectOfType<Player>().LoseLifeAction += OnPlayerLoseLife;

        spriteRenderer.color = shipData.UIColour.value;                 //debug
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
        if (systemResetCoroutine != null)
        {
            StopCoroutine(systemResetCoroutine);
        }

        systemResetCoroutine = RefreshEnemySystems(0);
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

            systemResetCoroutine = RefreshEnemySystems(currentSystemIndex);
            yield return systemResetCoroutine;

            Respawn();
        }
    }

    //disable and re-enable current systems upon player losing life
    void OnPlayerLoseLife()
    {
        int currentSystemIndex = shipData.MaxLives.Value - currentLives;

        SetInvincible(1f);

        if (systemResetCoroutine != null)
        {
            StopCoroutine(systemResetCoroutine);
        }

        systemResetCoroutine = RefreshEnemySystems(currentSystemIndex);
        StartCoroutine(systemResetCoroutine);
    }

    IEnumerator RefreshEnemySystems(int currentSystemIndex)
    {
        GetActiveEnemySystems();

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

        yield return WaitForSeconds(RespawnTime);

        nextBulletSystem.SetEnabled(true);
        nextMovementSystem.enabled = true;

        StartAttackAction?.Invoke(currentSystemIndex);
    }

    void GetActiveEnemySystems()
    {
        currentBulletSystems.Clear();

        //check all children of BulletSystem container for enabled IEnemyAttack
        for (int i = 0; i < bulletSystemContainer.childCount; i++)
        {
            Transform child = bulletSystemContainer.GetChild(i);

            //find bullet systems
            if (child.TryGetComponent(out IEnemyAttack bulletSystem))
            {
                //if a bullet system exists, regardless of its enabled status, find respective movement system
                currentMovementSystem = movementSystemContainer.GetChild(i).GetComponent<EnemyMovement>();

                if (bulletSystem.Enabled)
                {
                    currentBulletSystems.Add(bulletSystem);
                }
            }

            //check for active bullet subsystems
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

            //find movement system
            if (child.TryGetComponent(out EnemyMovement movementSystem) && movementSystem.enabled)
            {
                currentMovementSystem = movementSystem;
            }

            //if an active bullet system exists, immediately break out to avoid further checks
            if (currentBulletSystems.Count > 0) break;
        }
    }

    protected override void Die()
    {
        gameObject.SetActive(false);
    }
}