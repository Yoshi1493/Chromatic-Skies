using System;
using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PlayerSpecialShooter : Shooter<PlayerBullet>
{
    [SerializeField] FloatObject specialMeter;
    const float MaxSpecialMeter = 300f;
    const float SpecialThreshold = 100f;

    public event Action SpecialAction;
    public event Action SpecialReadyAction;
    public event Action SpecialMeterUpdateAction;
    bool canShoot = true;

    Enemy enemy;

    protected override float ShootingCooldown => 5f;

    protected override void Start()
    {
        base.Start();

        //put in Start instead of Awake due to script execution order conditions
        enemy = FindObjectOfType<Enemy>();

        ownerShip.TakeDamageAction += OnPlayerTakeDamage;
        enemy.TakeDamageAction += OnEnemyTakeDamage;
        specialMeter.value = SpecialThreshold;
    }

    void Update()
    {
        GetShootingInput();
    }

    void GetShootingInput()
    {
        if (Input.GetButtonDown("Special"))
        {
            if (canShoot && specialMeter.value >= SpecialThreshold)
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
        yield return WaitForSeconds(1f);

        SpawnProjectile(0, 0f, transform.position, false);
        GainSpecialMeter(-SpecialThreshold);

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
        GainSpecialMeter(2f);
    }

    void OnEnemyTakeDamage()
    {
        GainSpecialMeter(0.5f);
    }

}