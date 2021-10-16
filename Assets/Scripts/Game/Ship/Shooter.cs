using System.Collections;
using UnityEngine;

public abstract class Shooter<TProjectile> : MonoBehaviour
    where TProjectile : Projectile
{
    new protected Transform transform;
    protected Ship ownerShip;

    [SerializeField] protected FloatObject shootingSpeed;
    protected float ShootingSpeed => shootingSpeed.value;
    protected virtual float ShootingCooldown => ShootingSpeed == 0 ? 0 : 1 / ShootingSpeed;

    protected IEnumerator shootCoroutine;

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