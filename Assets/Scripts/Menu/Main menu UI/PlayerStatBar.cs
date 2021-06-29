using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PlayerStatBar : MonoBehaviour
{
    Image barImage;
    readonly AnimationCurve interpolationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    void Awake()
    {
        barImage = GetComponent<Image>();
    }

    public void LerpFillAmount(float endFill)
    {
        Run.Coroutine(_LerpFillAmount(Mathf.Clamp01(endFill)));
    }

    IEnumerator _LerpFillAmount(float endFill)
    {
        float currentFill = barImage.fillAmount;
        float currentLerpTime = 0f, totalLerpTime = 0.25f;

        while (currentFill != endFill)
        {
            float animationProgress = interpolationCurve.Evaluate(currentLerpTime / totalLerpTime);
            barImage.fillAmount = Mathf.Lerp(currentFill, endFill, animationProgress);

            yield return CoroutineHelper.EndOfFrame;
            currentLerpTime += Time.deltaTime;
        }
    }
}