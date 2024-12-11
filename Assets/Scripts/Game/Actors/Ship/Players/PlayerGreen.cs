using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PlayerGreen : Player
{
    [Space]

    [SerializeField] FloatObject originalShootingSpeed;
    [SerializeField] FloatObject rapidfireShootingSpeed;    

    IEnumerator rapidfireCoroutine;

    protected override void Awake()
    {
        base.Awake();
        shipData.ShootingSpeed.Variable = originalShootingSpeed;
    }

    protected override void OnSpecialActivated()
    {
        if (rapidfireCoroutine != null)
        {
            StopCoroutine(rapidfireCoroutine);
        }

        rapidfireCoroutine = IncreaseShootingSpeed();
        StartCoroutine(rapidfireCoroutine);
    }

    IEnumerator IncreaseShootingSpeed()
    {
        shipData.ShootingSpeed.Variable = rapidfireShootingSpeed;

        yield return WaitForSeconds(5f);

        shipData.ShootingSpeed.Variable = originalShootingSpeed;
    }
}