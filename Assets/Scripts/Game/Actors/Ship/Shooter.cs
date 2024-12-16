using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shooter<TProjectile> : MonoBehaviour
    where TProjectile : Projectile
{
    [SerializeField] List<TProjectile> projectiles;

    new protected Transform transform;
    protected Ship ownerShip;

    protected IEnumerator shootCoroutine;
    protected virtual float ShootingCooldown => 1 / ownerShip.shipData.ShootingSpeed.Value;

    protected virtual void Awake()
    {
        transform = GetComponent<Transform>();
        ownerShip = GetComponentInParent<Ship>();
    }

    protected virtual void OnEnable()
    {
        GenericObjectPool<TProjectile>.Instance.UpdatePoolableObjects(projectiles);
    }

    protected virtual void Start()
    {
        ownerShip.LoseLifeAction += OnLoseLife;
        ownerShip.DeathAction += OnDie;
    }

    protected abstract IEnumerator Shoot();

    public TProjectile SpawnProjectile(int projectileID, float spawnRotZ, Vector3 spawnPos, bool asLocalPosition = true)
    {
        TProjectile newProjectile = GenericObjectPool<TProjectile>.Instance.Get(projectileID);

        newProjectile.gameObject.SetActive(true);
        newProjectile.transform.SetPositionAndRotation(spawnPos + (asLocalPosition ? transform.position : Vector3.zero), Quaternion.Euler(0f, 0f, spawnRotZ));
        newProjectile.enabled = true;

        if (!char.IsNumber(newProjectile.name[^1]))                                     //debug
            newProjectile.name += $" {newProjectile.transform.GetSiblingIndex()}";

        return newProjectile;
    }

    //to-do: optimize?
    protected void DestroyAllProjectiles()
    {
        TProjectile[] projectiles = FindObjectsOfType<TProjectile>();

        for (int i = 0; i < projectiles.Length; i++)
        {
            projectiles[i].Destroy();

            if (projectiles[i] is Bullet)
            {
                Vector3 pos = projectiles[i].transform.position;
                projectiles[i].SpawnDestructionParticles(pos);
            }
        }
    }

    protected virtual void OnLoseLife()
    {
        DestroyAllProjectiles();
    }

    protected virtual void OnDie()
    {
        enabled = false;
    }
}