using System;
using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public interface IBossAttack
{
    string Name { get; }
    bool Enabled { get; }
    void SetEnabled(bool state);

    Action StartAttackLoopAction { get; set; }
    Action StartMoveAction { get; set; }
}

public abstract class BossShooter<TProjectile> : EnemyShooter<Boss, TProjectile>, IBossAttack
    where TProjectile : Projectile
{
    #region Interface impl.

    string IBossAttack.Name => name;
    bool IBossAttack.Enabled => enabled;
    void IBossAttack.SetEnabled(bool state) { enabled = state; }

    public Action StartAttackLoopAction { get; set; }
    public Action StartMoveAction { get; set; }

    #endregion

    protected override void Start()
    {
        base.Start();
        playerShip.LoseLifeAction += OnPlayerLoseLife;
    }

    protected override IEnumerator Shoot()
    {
        StartAttackLoopAction?.Invoke();
        AudioManager.Instance.PlaySound("boss_attack-charge");

        yield return WaitForSeconds(2f);
    }

    protected void SetSubsystemEnabled(int subsystemIndex)
    {
        if (transform.GetChild(subsystemIndex - 1).TryGetComponent(out IBossAttack subsystem))
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
    }

    void OnPlayerLoseLife()
    {
        StopAllCoroutines();
    }
}