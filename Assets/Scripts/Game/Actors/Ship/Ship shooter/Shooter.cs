using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shooter<TShip, TProjectile> : MonoBehaviour
    where TShip : Ship
    where TProjectile : Projectile
{
    [SerializeField] List<TProjectile> projectiles;

    new protected Transform transform;
    protected TShip parentShip;

    protected IEnumerator shootCoroutine;
    protected virtual float ShootingCooldown => 1 / parentShip.shipData.ShootingSpeed.Value;
    public bool CanShoot { get; protected set; }

    protected virtual void Awake()
    {
        transform = GetComponent<Transform>();
        parentShip = GetComponentInParent<TShip>();
    }

    protected virtual void OnEnable()
    { 
        ProjectileObjectPool<TProjectile>.Instance.UpdatePoolableObjects(projectiles);
        CanShoot = true;
    }

    protected virtual void Start()
    {
        parentShip.LoseLifeAction += OnLoseLife;
        parentShip.DeathAction += OnDie;
    }

    protected abstract IEnumerator Shoot();

    public TProjectile SpawnProjectile(int projectileID, float spawnRotZ, Vector3 spawnPos, bool asLocalPosition = true)
    {
        TProjectile newProjectile = ProjectileObjectPool<TProjectile>.Instance.Get(projectileID);

        newProjectile.gameObject.SetActive(true);
        newProjectile.transform.SetPositionAndRotation(spawnPos + (asLocalPosition ? transform.position : Vector3.zero), Quaternion.Euler(0f, 0f, spawnRotZ));
        newProjectile.enabled = true;

        if (!char.IsNumber(newProjectile.name[^1]))                                     //debug
            newProjectile.name += $" {newProjectile.transform.GetSiblingIndex()}";

        return newProjectile;
    }

    protected abstract void OnLoseLife();

    protected virtual void OnDie()
    {
        enabled = false;
    }

    protected virtual void OnDisable()
    {
        CanShoot = false;
    }
}