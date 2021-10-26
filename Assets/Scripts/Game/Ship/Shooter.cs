using System.Collections;
using UnityEngine;

public abstract class Shooter<TProjectile> : MonoBehaviour
    where TProjectile : Projectile
{
    new protected Transform transform;
    protected Ship ownerShip;

    protected IEnumerator shootCoroutine;
    protected virtual float ShootingCooldown => 0.1f;

    protected virtual void Awake()
    {
        transform = GetComponent<Transform>();
        ownerShip = GetComponentInParent<Ship>();
    }

    protected abstract IEnumerator Shoot();

    protected TProjectile SpawnProjectile(int projectileIndex, float spawnRotZ, Vector3 spawnPos, bool asLocalPosition = true)
    {
        TProjectile newProjectile = GenericObjectPool<TProjectile>.Instance.Get(projectileIndex);

        newProjectile.transform.SetPositionAndRotation(spawnPos + (asLocalPosition ? transform.position : Vector3.zero), Quaternion.Euler(0f, 0f, spawnRotZ));

        newProjectile.gameObject.SetActive(true);
        newProjectile.enabled = true;

        //newProjectile.name += $" {newProjectile.transform.GetSiblingIndex()}";        //debug

        return newProjectile;
    }

    //to-do: optimize?
    protected void DestroyAllProjectiles()
    {
        TProjectile[] projectiles = FindObjectsOfType<TProjectile>();

        for (int i = 0; i < projectiles.Length; i++)
        {
            projectiles[i].Destroy();
        }
    }
}