using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class Boss : CharacterShip
{
    [Space]
    [SerializeField] Transform bulletSystemContainer;
    [SerializeField] Transform movementSystemContainer;

    public List<IBossAttack> bulletSystems { get; private set; }
    List<BossMovement> movementSystems;

    public event Action<int> StartAttackAction;
    IEnumerator systemResetCoroutine;

    Player player;

    protected override void Awake()
    {
        base.Awake();
        SpriteRenderer.color = shipData.UIColour.value;                 //debug

        ValidateAttackSystems();

        player = FindObjectOfType<Player>();
    }

    void ValidateAttackSystems()
    {
        bulletSystems = new(shipData.MaxLives.Value);
        movementSystems = new(movementSystemContainer.childCount);

        for (int i = 0; i < bulletSystemContainer.childCount; i++)
        {
            if (bulletSystemContainer.GetChild(i).TryGetComponent(out IBossAttack bossAttack))
            {
                bulletSystems.Add(bossAttack);
            }
        }

        for (int i = 0; i < movementSystemContainer.childCount; i++)
        {
            if (movementSystemContainer.GetChild(i).TryGetComponent(out BossMovement bossMovement))
            {
                movementSystems.Add(bossMovement);
            }
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        SetInvincible(4f);
        RefreshBossSystems(RespawnTime);
    }

    void Start()
    {
        player.LoseLifeAction += OnPlayerLoseLife;
    }

#if UNITY_EDITOR
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            TakeDamage(currentHealth.value);
    }
#endif

    //disable current systems, and enable next systems upon losing life
    protected override IEnumerator LoseLife()
    {
        StartCoroutine(base.LoseLife());

        BossBulletPool.Instance.DrainPool();
        BossLaserPool.Instance.DrainPool();

        if (currentLives.value > 0)
        {
            RefreshBossSystems(RespawnTime);
        }

        yield return null;
    }

    //disable and re-enable current systems upon player losing life
    void OnPlayerLoseLife()
    {
        if (player.currentLives.value > 0)
        {
            SetInvincible(player.RespawnTime + 2f);
            RefreshBossSystems(player.RespawnTime);
        }
    }

    void RefreshBossSystems(float refreshTime)
    {
        if (systemResetCoroutine != null)
        {
            StopCoroutine(systemResetCoroutine);
        }

        systemResetCoroutine = _RefreshBossSystems(refreshTime);
        StartCoroutine(systemResetCoroutine);
    }

    IEnumerator _RefreshBossSystems(float refreshTime)
    {
        int currentSystemIndex = shipData.MaxLives.Value - currentLives.value;

        List<IBossAttack> currentBulletSystems = GetCurrentBulletSystem();
        BossMovement currentMovementSystem = GetCurrentMovementSystem();

        IBossAttack nextBulletSystem;
        BossMovement nextMovementSystem;

        //if player died, keep same attack+movement system
        if (currentHealth.value > 0)
        {
            nextBulletSystem = currentBulletSystems[0];
            nextMovementSystem = currentMovementSystem;
        }
        //otherwise, prepare next attack+movement system
        else
        {
            nextBulletSystem = bulletSystems[currentSystemIndex];
            nextMovementSystem = movementSystems[currentSystemIndex];
        }

        //disable attack+movement systems
        foreach (var bulletSystem in currentBulletSystems)
        {
            bulletSystem.SetEnabled(false);
        }
        currentMovementSystem.StopAllCoroutines();
        currentMovementSystem.enabled = false;

        yield return null;

        //enable next attack+movement systems
        nextMovementSystem.enabled = true;

        StartAttackAction?.Invoke(currentSystemIndex);
        yield return WaitForSeconds(refreshTime);

        nextBulletSystem.SetEnabled(true);
    }

    public List<IBossAttack> GetCurrentBulletSystem()
    {
        List<IBossAttack> currentBulletSystems = new();

        for (int i = 0; i < bulletSystemContainer.childCount; i++)
        {
            Transform child = bulletSystemContainer.GetChild(i);

            if (child.TryGetComponent(out IBossAttack bulletSystem) && bulletSystem.Enabled)
            {
                currentBulletSystems.Add(bulletSystem);

                if (child.childCount > 0)
                {
                    for (int ii = 0; ii < child.childCount; ii++)
                    {
                        if (child.GetChild(ii).TryGetComponent(out IBossAttack bulletSubsystem) && bulletSubsystem.Enabled)
                        {
                            currentBulletSystems.Add(bulletSubsystem);
                        }
                    }
                }

                break;
            }
        }

        //if no active bullet system found, add first by default
        if (currentBulletSystems.Count == 0)
        {
            currentBulletSystems.Add(bulletSystems[0]);
        }

        return currentBulletSystems;
    }

    public BossMovement GetCurrentMovementSystem()
    {
        for (int i = 0; i < movementSystems.Count; i++)
        {
            if (movementSystems[i].enabled)
            {
                return movementSystems[i];
            }
        }

        //if no movement system found, return current by default
        return movementSystems[shipData.MaxLives.Value - currentLives.value];
    }

    public void DisplayInvincibleShield(Vector3 spawnPos)
    {
        var particleEffect = VFXObjectPool.Instance.Get((int)VFXType.InvincibleBossShield);

        particleEffect.transform.position = transform.position;
        particleEffect.gameObject.SetActive(true);

        Vector2 highlightOffset = (spawnPos - transform.position).normalized;

        particleEffect.ParticleSystem.SetFloat("ParticleSize", InvincibleColliderRadius);
        particleEffect.ParticleSystem.SetVector4("ParticleColour", shipData.UIColour.value);
        particleEffect.ParticleSystem.SetVector2("HighlightOffset", highlightOffset);

        particleEffect.enabled = true;
    }

    protected override IEnumerator Die()
    {
        yield return base.Die();

        enabled = false;
        collider.enabled = false;
    }
}