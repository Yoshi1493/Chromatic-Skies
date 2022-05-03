using System.Collections;
using UnityEngine;

public abstract class Shooter<TProjectile> : MonoBehaviour
    where TProjectile : Projectile
{
    new protected Transform transform;
    protected Ship ownerShip;

    protected IEnumerator shootCoroutine;
    protected virtual float ShootingCooldown => 1 / ownerShip.shipData.ShootingSpeed.Value;

    protected virtual void Awake()
    {
        transform = GetComponent<Transform>();
        ownerShip = GetComponentInParent<Ship>();
    }

    protected abstract IEnumerator Shoot();

    public TProjectile SpawnProjectile(int projectileIndex, float spawnRotZ, Vector3 spawnPos, bool asLocalPosition = true)
    {
        TProjectile newProjectile = GenericObjectPool<TProjectile>.Instance.Get(projectileIndex);

        newProjectile.gameObject.SetActive(true);
        newProjectile.transform.SetPositionAndRotation(spawnPos + (asLocalPosition ? transform.position : Vector3.zero), Quaternion.Euler(0f, 0f, spawnRotZ));
        newProjectile.enabled = true;

        newProjectile.name += $" {newProjectile.transform.GetSiblingIndex()}";        //debug

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