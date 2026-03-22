using System;
using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public abstract class PlayerSpecialShooter : Shooter<Player, SpecialBullet>
{
    [Space]

    [SerializeField] FloatObject specialMeter;
    public const float MaxSpecialMeter = 3f;
    const float InitialSpecialMeter = 1f;
    const float SpecialCost = 1f;
    const float MeterGainPerBossHit = 0.005f;
    const float MeterGainPerHit = 0.02f;
    const float MeterGainPerGraze = 0.002f;

    public event Action SpecialAction;
    public event Action SpecialReadyAction;
    public event Action SpecialMeterUpdateAction;

    Boss boss;

    protected virtual float SpecialCooldown => 5f;

    protected override void Start()
    {
        base.Start();

        //put in Start instead of Awake due to script execution order conditions
        boss = FindObjectOfType<Boss>();
        boss.TakeDamageAction += OnBossTakeDamage;
        boss.DeathAction += OnBossDie;

        parentShip.TakeDamageAction += OnPlayerTakeDamage;
        parentShip.GetComponentInChildren<PlayerGraze>().GrazeAction += OnPlayerGraze;

        specialMeter.value = InitialSpecialMeter;
    }

    void Update()
    {
        if (!PauseHandler.IsPaused)
        {
            GetShootingInput();
        }
    }

    void GetShootingInput()
    {
        if (Input.GetButtonDown("Special"))
        {
            if (CanShoot && specialMeter.value >= SpecialCost)
            {
                if (shootCoroutine != null)
                {
                    StopCoroutine(shootCoroutine);
                }

                shootCoroutine = Shoot();
                StartCoroutine(shootCoroutine);
            }
        }
    }

    //proper shooting functionality handled in child classes
    protected override IEnumerator Shoot()
    {
        SpecialAction?.Invoke();

        GainSpecialMeter(-SpecialCost);

        CanShoot = false;
        yield return null;
    }

    void GainSpecialMeter(float amount)
    {
        specialMeter.value = Mathf.Clamp(specialMeter.value + amount, 0f, MaxSpecialMeter);
        SpecialMeterUpdateAction?.Invoke();
    }

    void OnPlayerTakeDamage(int damage)
    {
        if (damage > 0)
        {
            GainSpecialMeter(MeterGainPerHit);
        }
    }
    protected override void OnLoseLife()
    {
        StopAllCoroutines();
        enabled = false;
    }

    void OnRespawn()
    {
        enabled = true;
    }

    void OnBossTakeDamage(int _)
    {
        GainSpecialMeter(MeterGainPerBossHit);
    }

    void OnBossDie()
    {
        enabled = false;
    }

    void OnPlayerGraze()
    {
        GainSpecialMeter(MeterGainPerGraze);
    }
}