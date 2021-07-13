using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public static class ProjectileBehaviour
{
    public static IEnumerator ChangeSpeed(this Projectile p, float startSpeed, float endSpeed, float lerpTime, float delay = 0f)
    {
        float currentLerpTime = 0f;

        if (delay > 0) yield return WaitForSeconds(delay);

        while (currentLerpTime < lerpTime)
        {
            p.MoveSpeed = Mathf.Lerp(startSpeed, endSpeed, currentLerpTime / lerpTime);

            yield return EndOfFrame;
            currentLerpTime += Time.deltaTime;
        }
    }

    public static IEnumerator RotateBy(this Projectile p, float rotateAmount, float lerpTime, float delay = 0f)
    {
        float currentLerpTime = 0f;
        float startRotation = p.transform.eulerAngles.z;
        float endRotation = startRotation + rotateAmount;

        if (delay > 0) yield return WaitForSeconds(delay);

        while(currentLerpTime < lerpTime)
        {
            float zRotation = Mathf.Lerp(startRotation, endRotation, currentLerpTime / lerpTime);
            p.transform.eulerAngles = Vector3.forward * zRotation;

            yield return EndOfFrame;
            currentLerpTime += Time.deltaTime;
        }
    }

    public static IEnumerator TurnTowards(this Projectile p, Actor target, float lerpTime, float delay = 0f)
    {
        if (target == null) yield break;

        float currentLerpTime = 0f;
        float zRotation = GetRotationDifference(p.transform.position, target.transform.position);

        while (currentLerpTime < lerpTime)
        {
            yield return p.RotateBy(zRotation, lerpTime, delay);
        }
    }

    public static IEnumerator HomeInOn(this Projectile p, Actor target, float homingDuration, float delay = 0f)
    {
        if (target == null || homingDuration <= 0f) yield break;

        float currentTime = 0f;
        
        while (currentTime < homingDuration)
        {
            float zRotation = GetRotationDifference(p.transform.position, target.transform.position);
            p.transform.eulerAngles = Vector3.forward;

            yield return EndOfFrame;
            currentTime += Time.deltaTime;
        }
    }

    static float GetRotationDifference(Vector2 pos1, Vector2 pos2)
    {
        Vector2 distance = pos2 - pos1;
        float difference = Mathf.Atan2(-distance.x, distance.y) * Mathf.Rad2Deg;

        return difference;
    }
}