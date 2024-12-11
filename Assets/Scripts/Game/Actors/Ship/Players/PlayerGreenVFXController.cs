using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PlayerGreenVFXController : PlayerParticleController
{
    [Space]

    [SerializeField] PlayerSpecialShooter specialShooter;
    [SerializeField] SpriteRenderer afterimageSheet;
    IEnumerator afterimageCoroutine;

    protected override void Awake()
    {
        base.Awake();
        specialShooter.SpecialAction += OnSpecialActivated;
    }

    void OnSpecialActivated()
    {
        if (afterimageCoroutine != null)
        {
            StopCoroutine(afterimageCoroutine);
        }

        afterimageCoroutine = DisplayAfterimageTrail();
        StartCoroutine(afterimageCoroutine);
    }

    IEnumerator DisplayAfterimageTrail()
    {
        float currentTime = 0f;
        float totalDisplayTime = 4.5f;
        float displayInterval = 0.1f;

        while (currentTime < totalDisplayTime)
        {
            //to-do: impl.
            yield return WaitForSeconds(displayInterval);
            currentTime += displayInterval;
        }
    }
}