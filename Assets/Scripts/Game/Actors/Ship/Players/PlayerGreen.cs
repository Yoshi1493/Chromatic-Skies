using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PlayerGreen : Player
{
    [Space]

    [SerializeField] FloatObject originalShootingSpeed;
    [SerializeField] FloatObject rapidfireShootingSpeed;

    IEnumerator rapidfireCoroutine;
    IEnumerator afterimageCoroutine;

    protected override void Awake()
    {
        base.Awake();
        shipData.ShootingSpeed.Variable = originalShootingSpeed;
    }

    //to-do
    protected override void OnSpecialActivated()
    {
        if (rapidfireCoroutine != null)
        {
            StopCoroutine(rapidfireCoroutine);
        }

        if (afterimageCoroutine != null)
        {
            StopCoroutine(afterimageCoroutine);
        }

        rapidfireCoroutine = IncreaseShootingSpeed();
        afterimageCoroutine = DisplayAfterimageTrail();

        StartCoroutine(rapidfireCoroutine);
        StartCoroutine(afterimageCoroutine);
    }

    IEnumerator IncreaseShootingSpeed()
    {
        shipData.ShootingSpeed.Variable = rapidfireShootingSpeed;

        yield return WaitForSeconds(5f);

        shipData.ShootingSpeed.Variable = originalShootingSpeed;
    }

    IEnumerator DisplayAfterimageTrail()
    {
        //SpriteRenderer
        yield return WaitForSeconds(4.5f);
    }
}