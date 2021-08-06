using System.Collections;
using UnityEngine;

public abstract class EnemyShooter : Shooter
{
    protected virtual void OnEnable()
    {
        if (shootCoroutine != null) StopCoroutine(shootCoroutine);
        shootCoroutine = Shoot();
        StartCoroutine(shootCoroutine);
    }

    protected override IEnumerator Shoot()
    {
        yield return CoroutineHelper.WaitForSeconds(1f);
    }
}