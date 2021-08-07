using System.Collections;
using UnityEngine;

public abstract class Shooter : MonoBehaviour
{
    protected Ship ownerShip;
    protected Vector2 ShipPosition => ownerShip.transform.position;

    [SerializeField] protected FloatReference shootingSpeed;
    protected float ShootingSpeed => shootingSpeed.CurrentValue;
    protected virtual float ShootingCooldown => ShootingSpeed == 0 ? 0 : 1 / ShootingSpeed;

    protected IEnumerator shootCoroutine;

    protected virtual void Awake()
    {
        ownerShip = GetComponentInParent<Ship>();
    }

    protected abstract IEnumerator Shoot();

    //to-do: optimize?
    protected void DestroyAllBullets<T>() where T : Bullet
    {
        T[] bullets = FindObjectsOfType<T>();

        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i].Destroy();
        }
    }
}