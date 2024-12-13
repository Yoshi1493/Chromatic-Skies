using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PlayerGreenVFXController : PlayerParticleController
{
    [Space]

    [SerializeField] PlayerSpecialShooter specialShooter;
    IEnumerator afterimageCoroutine;

    protected override void Awake()
    {
        base.Awake();

        specialShooter.SpecialAction += OnSpecialActivated;
        specialVFX.SetVector4("ParticleColour", parentShip.shipData.UIColour.value);
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
        transform.parent = null;

        float currentTime = 0f;
        float totalDisplayTime = 4f;

        PlayVisualEffect(specialVFX);
        specialVFX.SetVector2("OriginalPosition", parentShip.transform.position);

        while (currentTime < totalDisplayTime)
        {
            specialVFX.SetVector2("SpawnPosition", parentShip.transform.position);

            yield return null;
            currentTime += Time.deltaTime;
        }

        yield return WaitUntil(() => specialVFX.aliveParticleCount == 0);
        transform.parent = parentShip.transform;
        transform.localPosition = Vector3.zero;

        StopVisualEffect(specialVFX);
    }
}