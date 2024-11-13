using System;
using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PlayerSpecialShooter : PlayerShooter
{
    [Space]

    [SerializeField] FloatObject specialMeter;
    public const float MaxSpecialMeter = 3f;
    const float SpecialCost = 1f;
    const float MeterGainPerEnemyHit = 0.005f;
    const float MeterGainPerHit = 0.02f;
    const float MeterGainPerGraze = 0.002f;

    public event Action SpecialAction;
    public event Action SpecialReadyAction;
    public event Action SpecialMeterUpdateAction;

    Enemy enemy;

    protected override float ShootingCooldown => 5f;

    protected override void Start()
    {
        base.Start();

        //put in Start instead of Awake due to script execution order conditions
        enemy = FindObjectOfType<Enemy>();
        enemy.TakeDamageAction += OnEnemyTakeDamage;

        ownerShip.TakeDamageAction += OnPlayerTakeDamage;
        ownerShip.GetComponentInChildren<PlayerGraze>().GrazeAction += OnPlayerGraze;

        specialMeter.value = SpecialCost;
    }

    void Update()
    {
        GetShootingInput();
    }

    void GetShootingInput()
    {
        if (Input.GetButtonDown("Special"))
        {
            if (canShoot && specialMeter.value >= SpecialCost)
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

    protected override IEnumerator Shoot()
    {
        SpecialAction?.Invoke();
        //yield return WaitForSeconds(1f);

        SpawnProjectile(1, 0f, transform.position, false);
        GainSpecialMeter(-SpecialCost);

        canShoot = false;
        yield return WaitForSeconds(ShootingCooldown);

        canShoot = true;
    }

    void GainSpecialMeter(float amount)
    {
        specialMeter.value = Mathf.Clamp(specialMeter.value + amount, 0f, MaxSpecialMeter);
        SpecialMeterUpdateAction?.Invoke();
    }

    void OnPlayerTakeDamage()
    {
        GainSpecialMeter(MeterGainPerHit);
    }

    void OnEnemyTakeDamage()
    {
        GainSpecialMeter(MeterGainPerEnemyHit);
    }

    void OnPlayerGraze()
    {
        GainSpecialMeter(MeterGainPerGraze);
    }
}