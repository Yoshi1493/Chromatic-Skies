using System.Collections;
using UnityEngine;

public class BossSpriteController : ShipSpriteController<Boss>
{
    protected override void Start()
    {
        base.Start();
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

            yield return null;
            currentLerpTime += Time.deltaTime;
        }

        SetSpriteAlpha(1f);
    }
}