using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

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

    protected override void Awake()
    {
        base.Awake();

        ValidateAttackSystems();
        FindObjectOfType<Player>().LoseLifeAction += OnPlayerLoseLife;
    }

    void ValidateAttackSystems()
    {
        bulletSystems = new(bulletSystemContainer.childCount);
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

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.K))
            TakeDamage(currentHealth);
#endif
    }

    //disable current systems, and enable next systems upon losing life
    protected override async void LoseLife()
    {
        int currentAttackSystemIndex = shipData.MaxLives.Value - currentLives;
        await Task.Yield();

        base.LoseLife();
        EnemyBulletPool.Instance.DrainPool();
        EnemyLaserPool.Instance.DrainPool();

        if (currentLives > 0)
        {
            GetActiveEnemySystems();
            nextBulletSystem = bulletSystems[currentAttackSystemIndex + 1];
            nextMovementSystem = movementSystems[currentAttackSystemIndex + 1];

            foreach (var bulletSystem in currentBulletSystems)
            {
                bulletSystem.SetEnabled(false);
            }

            currentMovementSystem.StopAllCoroutines();
            currentMovementSystem.enabled = false;

            await Task.Delay(RespawnTime);
            Respawn();

            nextBulletSystem.SetEnabled(true);
            nextMovementSystem.enabled = true;
            collider.enabled = true;
        }
    }

    //disable and re-enable current systems upon player losing life
    async void OnPlayerLoseLife()
    {
        GetActiveEnemySystems();

        foreach (var bulletSystem in currentBulletSystems)
        {
            bulletSystem.SetEnabled(false);
        }

        currentMovementSystem.StopAllCoroutines();
        currentMovementSystem.enabled = false;

        SetInvincible(1f);

        await Task.Delay(RespawnTime);

        if (currentBulletSystems.Count > 0)
        {
            currentBulletSystems[0].SetEnabled(true);
            currentMovementSystem.enabled = true;
        }
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