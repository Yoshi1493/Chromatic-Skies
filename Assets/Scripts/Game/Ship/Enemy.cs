using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class Enemy : Ship
{
    [Header("Enemy-specific")]
    [SerializeField] Transform bulletSystemContainer;
    [SerializeField] Transform movementSystemContainer;

    public List<IEnemyAttack> bulletSystems { get; private set; }
    List<EnemyMovement> movementSystems;

    IEnumerator systemResetCoroutine;

    int CurrentAttackSystem => shipData.MaxLives.Value - currentLives;

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
                enemyAttack.SetEnabled(false);
            }
        }

        for (int i = 0; i < movementSystemContainer.childCount; i++)
        {
            if (movementSystemContainer.GetChild(i).TryGetComponent(out EnemyMovement enemyMovement))
            {
                movementSystems.Add(enemyMovement);
                enemyMovement.enabled = false;
            }
        }
    }

    void OnEnable()
    {
        bulletSystems[CurrentAttackSystem].SetEnabled(true);
        movementSystems[CurrentAttackSystem].enabled = true;
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.K))
            TakeDamage(currentHealth);
#endif
    }

    //disable current systems, and enable next systems upon losing life
    protected override IEnumerator LoseLife()
    {
        StartCoroutine(base.LoseLife());

        EnemyBulletPool.Instance.DrainPool();
        EnemyLaserPool.Instance.DrainPool();

        if (currentLives > 0)
        {
            if (systemResetCoroutine != null)
            {
                StopCoroutine(systemResetCoroutine);
            }

            systemResetCoroutine = RefreshEnemySystems();
            yield return systemResetCoroutine;

            Respawn();
        }
    }

    //disable and re-enable current systems upon player losing life
    void OnPlayerLoseLife()
    {
        SetInvincible(1f);

        if (systemResetCoroutine != null)
        {
            StopCoroutine(systemResetCoroutine);
        }

        systemResetCoroutine = RefreshEnemySystems();
        StartCoroutine(systemResetCoroutine);
    }

    IEnumerator RefreshEnemySystems()
    {
        int currentSystemIndex = CurrentAttackSystem - (currentHealth > 0 ? 0 : 1);
        int nextSystemIndex = currentSystemIndex + (currentHealth > 0 ? 0 : 1);

        //get current bullet system(s)
        List<IEnemyAttack> currentBulletSystems = new() { bulletSystems[currentSystemIndex] };

        //get bullet subsystems if they exist
        foreach (Transform t in bulletSystemContainer.GetChild(currentSystemIndex))
        {
            if (t.TryGetComponent(out IEnemyAttack bulletSystem))
            {
                currentBulletSystems.Add(bulletSystem);
            }
        }

        //get current movement system
        EnemyMovement currentMovementSystem = movementSystems[currentSystemIndex];

        //get next bullet + movement systems
        IEnemyAttack nextBulletSystem = bulletSystems[nextSystemIndex];
        EnemyMovement nextMovementSystem = movementSystems[nextSystemIndex];

        //disable all bullet systems
        foreach (var bulletSystem in currentBulletSystems)
        {
            bulletSystem.SetEnabled(false);
        }

        //disable all movement systems
        currentMovementSystem.StopAllCoroutines();
        currentMovementSystem.enabled = false;

        yield return WaitForSeconds(RespawnTime);

        //enable next bullet + movement systems
        nextBulletSystem.SetEnabled(true);
        nextMovementSystem.enabled = true;
    }

    protected override void Die()
    {
        gameObject.SetActive(false);
    }
}