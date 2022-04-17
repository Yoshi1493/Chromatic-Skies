using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PlayerStatBar : MonoBehaviour
{
    Image statBarImage;

    IEnumerator statBarAnimation;
    const float AnimationDuration = 0.25f;
    readonly AnimationCurve interpolationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    void Awake()
    {
        statBarImage = GetComponent<Image>();
    }

    public void AnimateStatBar(float endFillAmount, Color endColour)
    {
        if (statBarAnimation != null)
            StopCoroutine(statBarAnimation);

        statBarAnimation = _AnimateStatBar(endFillAmount, endColour);
        StartCoroutine(statBarAnimation);
    }

    IEnumerator _AnimateStatBar(float endFillAmount, Color endColour)
    {
        float startFillAmount = statBarImage.fillAmount;
        Color startColour = statBarImage.color;

        float currentLerpTime = 0f;

        while (currentLerpTime <= AnimationDuration)
        {
            float animationProgress = interpolationCurve.Evaluate(currentLerpTime / AnimationDuration);

            statBarImage.fillAmount = Mathf.Lerp(startFillAmount, endFillAmount, animationProgress);
            statBarImage.color = Color.Lerp(startColour, endColour, animationProgress);

            currentLerpTime += Time.deltaTime;
            yield return CoroutineHelper.EndOfFrame;
        }

        statBarAnimation = null;
    }
}