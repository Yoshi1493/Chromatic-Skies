using System.Collections;
using UnityEngine;

public abstract class Shooter : MonoBehaviour
{
    protected Ship ownerShip;
    protected Vector3 ShipPosition => ownerShip.transform.position;

    [SerializeField] protected FloatObject shootingSpeed;
    protected float ShootingSpeed => shootingSpeed.value;
    protected virtual float ShootingCooldown => ShootingSpeed == 0 ? 0 : 1 / ShootingSpeed;

    public delegate void AttackStart();
    public AttackStart AttackStartAction;

    protected IEnumerator shootCoroutine;

    protected virtual void Awake()
    {
        ownerShip = GetComponentInParent<Ship>();
    }

    protected abstract IEnumerator Shoot();

    //to-do: optimize?
    protected void DestroyAllProjectiles<T>() where T : Projectile
    {
        T[] projectiles = FindObjectsOfType<T>();

        for (int i = 0; i < projectiles.Length; i++)
        {
            projectiles[i].Destroy();
        }
    }
}