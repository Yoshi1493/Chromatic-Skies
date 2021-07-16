using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public static class ProjectileBehaviour
{
    #region Basic behaviour

    /// <summary>
    /// interpolates <p.MoveSpeed> from <startSpeed> to <endSpeed>, in <lerpTime> seconds.
    /// </summary>
    public static IEnumerator ChangeSpeed(this Projectile p, float startSpeed, float endSpeed, float lerpTime, float delay = 0f)
    {
        if (delay > 0f) yield return WaitForSeconds(delay);

        float currentLerpTime = 0f;

        while (currentLerpTime < lerpTime)
        {
            p.MoveSpeed = Mathf.Lerp(startSpeed, endSpeed, currentLerpTime / lerpTime);

            currentLerpTime += Time.deltaTime;
            yield return EndOfFrame;
        }
    }

    /// <summary>
    /// interpolates <p.transformm.eulerAngles.z> from <p.transform.eulerAngles.z> to <p.transform.eulerAngles.z + rotateAmount>, in <lerpTime> seconds.
    /// </summary>
    public static IEnumerator RotateBy(this Projectile p, float rotateAmount, float lerpTime, float delay = 0f)
    {
        if (lerpTime <= 0f) yield break;
        if (delay > 0f) yield return WaitForSeconds(delay);

        float currentLerpTime = 0f;
        float startRotation = p.transform.eulerAngles.z;
        float endRotation = startRotation + rotateAmount;

        while (currentLerpTime < lerpTime)
        {
            float zRotation = Mathf.Lerp(startRotation, endRotation, currentLerpTime / lerpTime);
            p.transform.eulerAngles = Vector3.forward * zRotation;

            currentLerpTime += Time.deltaTime;
            yield return EndOfFrame;
        }
    }

    /// <summary>
    /// translates <p> such that it looks like it is orbiting around <target.transform.position>, at <rotateSpeed> degrees per second, for <rotateDuration> seconds.
    /// </summary>
    public static IEnumerator RotateAround(this Projectile p, Actor target, float rotateDuration, float rotateSpeed, float delay = 0f)
    {
        if (target == null || rotateDuration <= 0f) yield break;
        if (delay > 0f) yield return WaitForSeconds(delay);

        float currentTime = 0f;

        Vector3 newPos = p.transform.position;
        Vector3 difference = newPos - target.transform.position;

        while (currentTime < rotateDuration)
        {
            difference.RotateByDegrees(rotateSpeed * Time.deltaTime);
            p.transform.position = newPos + difference;

            currentTime += Time.deltaTime;
            yield return EndOfFrame;
        }
    }

    #endregion

    #region Combined/Complex behaviours

    /// <summary>
    /// rotates <p> to face towards <target.transform.position> in <lerpTime> seconds.
    /// i.e. rotates <p> in a way such that if <p> were to translate by <p.transform.up>, and
    /// <target.transform.position> remained the same, <p> will eventually collide with <target>
    /// </summary>
    public static IEnumerator TurnTowards(this Projectile p, Actor target, float lerpTime, float delay = 0f)
    {
        if (target == null || lerpTime <= 0f) yield break;

        float zRotation = GetRotationDifference(p.transform.position, target.transform.position);
        yield return p.RotateBy(zRotation, lerpTime, delay);
    }

    /// <summary>
    /// rotates <p> to continuously turn towards <target.transform.position> for <homingDuration> seconds
    /// </summary>
    public static IEnumerator HomeInOn(this Projectile p, Actor target, float homingDuration, float delay = 0f)
    {
        if (target == null || homingDuration <= 0f) yield break;
        if (delay > 0f) yield return WaitForSeconds(delay);

        float currentTime = 0f;

        while (currentTime < homingDuration)
        {
            float zRotation = GetRotationDifference(p.transform.position, target.transform.position);
            p.transform.eulerAngles = Vector3.forward;          //to-do: fix

            currentTime += Time.deltaTime;
            yield return EndOfFrame;
        }
    }

    #endregion

    #region Helpers/Extensions

    static float GetRotationDifference(Vector2 pos1, Vector2 pos2)
    {
        Vector2 distance = pos2 - pos1;
        float difference = Mathf.Atan2(-distance.x, distance.y) * Mathf.Rad2Deg;

        return difference;
    }

    static void RotateByDegrees(this Vector3 v, float theta)
    {
        Vector3 cached_v = v;
        float theta_r = theta * Mathf.Deg2Rad;

        v.x = (Mathf.Cos(theta_r) * cached_v.x) - (Mathf.Sin(theta_r) * cached_v.y);
        v.y = (Mathf.Sin(theta_r) * cached_v.x) + (Mathf.Cos(theta_r) * cached_v.y);
    }

    #endregion
}