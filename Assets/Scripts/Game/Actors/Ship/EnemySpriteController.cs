using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class EnemySpriteController : ShipSpriteController<Enemy>
{
    protected override void Awake()
    {
        base.Awake();
        ship.StartAttackAction += FadeInSprite;
    }

    void FadeInSprite(int attackIndex)
    {
        if (attackIndex == 0)
        {
            StartCoroutine(_FadeInSprite());
        }
    }

    IEnumerator _FadeInSprite()
    {
        float currentLerpTime = 0f;
        float totalLerpTime = 1f;

        while (currentLerpTime < totalLerpTime)
        {
            SetSpriteAlpha(currentLerpTime);

            yield return EndOfFrame;
            currentLerpTime += Time.deltaTime;
        }

        SetSpriteAlpha(1f);
    }
}