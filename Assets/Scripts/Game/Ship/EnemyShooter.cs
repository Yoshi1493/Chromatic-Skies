using System;
using System.Collections;
using UnityEngine;

public interface INamedAttack
{
    Action AttackStartAction { get; set; }
}

public abstract class EnemyShooter<TProjectile> : Shooter, INamedAttack
    where TProjectile : Projectile
{
    Player playerShip;
    protected Vector2 PlayerPosition => playerShip.transform.position;

    public Action AttackStartAction { get; set; }

    protected virtual void Start()
    {
        playerShip = FindObjectOfType<Player>();
        ownerShip.LoseLifeAction += OnLoseLife;
    }

    protected virtual void OnEnable()
    {
        if (shootCoroutine != null) StopCoroutine(shootCoroutine);
        shootCoroutine = Shoot();
        StartCoroutine(shootCoroutine);
    }

    protected override IEnumerator Shoot()
    {
        yield return CoroutineHelper.WaitForSeconds(1f);
        AttackStartAction?.Invoke();
    }

    protected void SetSubsystemEnabled(int subsystemIndex)
    {
        if (transform.GetChild(subsystemIndex - 1).TryGetComponent(out EnemyShooter<TProjectile> enemyShooter))
            enemyShooter.enabled = true;
    }

    protected TProjectile SpawnProjectile(int projectileIndex, float spawnRotZ, Vector3 spawnPos, bool asLocalPosition = true)
    {
        TProjectile newProjectile = GenericObjectPool<TProjectile>.Instance.Get(projectileIndex);

        newProjectile.transform.SetPositionAndRotation(spawnPos + (asLocalPosition ? transform.position : Vector3.zero), Quaternion.Euler(0f, 0f, spawnRotZ));

        newProjectile.gameObject.SetActive(true);
        newProjectile.enabled = true;

        return newProjectile;
    }

    protected virtual void OnLoseLife()
    {
        StopCoroutine(shootCoroutine);
        DestroyAllProjectiles<TProjectile>();
    }
}