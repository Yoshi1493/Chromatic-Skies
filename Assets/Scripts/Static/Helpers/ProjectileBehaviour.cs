using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public static class ProjectileBehaviour
{
    #region Movement behaviour

    /// <summary>
    /// lerps <p.MoveSpeed> from <startSpeed> to <endSpeed>, in <lerpTime> seconds.
    /// </summary>
    public static IEnumerator LerpSpeed(this Projectile p, float startSpeed, float endSpeed, float lerpTime, float delay = 0f)
    {
        if (delay > 0f) yield return WaitForSeconds(delay);

        if (lerpTime > 0f)
        {
            float currentLerpTime = 0f;

            while (currentLerpTime < lerpTime)
            {
                p.MoveSpeed = Mathf.Lerp(startSpeed, endSpeed, currentLerpTime / lerpTime);

                currentLerpTime += Time.deltaTime;
                yield return EndOfFrame;
            }
        }

        p.MoveSpeed = endSpeed;
    }

    /// <summary>
    /// (unused) uses Vector3.SmoothDamp to lerp <p.moveDirection> from <p.moveDirection> to <endDirection>, in <lerpTime> seconds.
    /// </summary>
    public static IEnumerator LerpDirection(this Projectile p, Vector3 endDirection, float lerpTime, float delay = 0f)
    {
        if (delay > 0f) yield return WaitForSeconds(delay);

        if (lerpTime > 0f)
        {
            Vector3 vel = p.moveDirection;

            while (p.moveDirection != endDirection)
            {
                p.moveDirection = Vector3.SmoothDamp(p.moveDirection, endDirection, ref vel, lerpTime);
                yield return EndOfFrame;
            }
        }

        p.moveDirection = endDirection;
    }

    /// <summary>
    /// rotates <p.moveDirection> upon the x-y plane by <degrees> degrees, over <rotateDuration> seconds.
    /// </summary>
    public static IEnumerator RotateBy(this Projectile p, float degrees, float rotateDuration, bool clockwise = true, float delay = 0f)
    {
        if (rotateDuration <= 0f) yield break;
        if (delay > 0f) yield return WaitForSeconds(delay);

        float currentTime = 0f;

        Vector3 startDir = p.moveDirection;

        while (currentTime < rotateDuration)
        {
            float degreesPerFrame = (degrees * (clockwise ? -1 : 1)) / rotateDuration * Time.deltaTime;
            RotateVectorBy(ref p.moveDirection, degreesPerFrame);

            currentTime += Time.deltaTime;
            yield return EndOfFrame;
        }

        p.moveDirection = startDir.RotateVectorBy(degrees);
    }

    /// <summary>
    /// rotates <p> around <target.transform.position> by setting <p.MoveSpeed> and <p.moveDirection>.
    /// rotates by <degreesPerSecond> degrees per second, for <rotateDuration> seconds
    /// </summary>
    public static IEnumerator RotateAround(this Projectile p, Actor target, float rotateDuration, float degreesPerSecond, bool clockwise = true, float delay = 0f)
    {
        if (target == null || rotateDuration <= 0f) yield break;
        if (delay > 0f) yield return WaitForSeconds(delay);

        float currentTime = 0f;

        Vector3 direction = p.transform.position - target.transform.position;
        float distance = direction.magnitude;

        while (currentTime < rotateDuration)
        {
            RotateVectorBy(ref p.moveDirection, degreesPerSecond * (clockwise ? -1 : 1) * Time.deltaTime);
            p.MoveSpeed = distance * (degreesPerSecond / Mathf.Rad2Deg);

            currentTime += Time.deltaTime;
            yield return EndOfFrame;
        }

        p.moveDirection = direction.RotateVectorBy(rotateDuration * degreesPerSecond);
    }

    /// <summary>
    /// overload of RotateAround() that takes in a Vector3 to rotate around, instead of an Actor.
    /// </summary>
    public static IEnumerator RotateAround(this Projectile p, Vector3 targetPosition, float rotateDuration, float degreesPerSecond, bool clockwise = true, float delay = 0f)
    {
        if (rotateDuration <= 0f) yield break;
        if (delay > 0f) yield return WaitForSeconds(delay);

        float currentTime = 0f;

        Vector3 direction = p.transform.position - targetPosition;
        float distance = direction.magnitude;

        while (currentTime < rotateDuration)
        {
            RotateVectorBy(ref p.moveDirection, degreesPerSecond * (clockwise ? -1 : 1) * Time.deltaTime);
            p.MoveSpeed = distance * (degreesPerSecond / Mathf.Rad2Deg);

            currentTime += Time.deltaTime;
            yield return EndOfFrame;
        }

        p.moveDirection = direction.RotateVectorBy(rotateDuration * degreesPerSecond);
    }

    /// <summary>
    /// translates <p> such that it looks like it is orbiting <target.transform.position>, at <rotateSpeed> degrees per second, for <rotateDuration> seconds.
    /// </summary>
    public static IEnumerator TranslateAround(this Projectile p, Actor target, float rotateDuration, float degreesPerSecond, bool followTarget = false, float delay = 0f)
    {
        if (target == null || rotateDuration <= 0f) yield break;
        if (delay > 0f) yield return WaitForSeconds(delay);

        float currentTime = 0f;
        Vector3 targetPos = target.transform.position;

        while (currentTime < rotateDuration)
        {
            if (followTarget) targetPos = target.transform.position;

            Vector3 distance = p.transform.position - targetPos;
            Vector3 difference = distance.RotateVectorBy(degreesPerSecond * Time.deltaTime);

            p.transform.position = targetPos + difference;
            currentTime += Time.deltaTime;
            yield return EndOfFrame;
        }
    }

    /// <summary>
    /// sets <p.moveDirection> to face towards <target.transform.position>
    /// i.e. sets <p.moveDirection> such that if <target.transform.position> doesn't change,
    /// <p> will eventually collide with <target>.
    /// </summary>
    public static Vector3 LookAt(this Projectile p, Actor target)
    {
        if (target != null)
        {
            var newMoveDirection = target.transform.position - p.transform.position;
            newMoveDirection.z = 0f;

            if (newMoveDirection != Vector3.zero)
            {
                p.moveDirection = newMoveDirection;
                return p.moveDirection;
            }

            else return p.moveDirection;
        }

        else return p.moveDirection;
    }

    /// <summary>
    /// overload of LookAt() that makes <p> face towards a given Vector3.
    /// </summary>
    public static Vector3 LookAt(this Projectile p, Vector3 targetPos)
    {
        var newMoveDirection = targetPos - p.transform.position;
        newMoveDirection.z = 0f;

        if (newMoveDirection != Vector3.zero)
        {
            p.moveDirection = newMoveDirection;
            return p.moveDirection;
        }

        else return p.moveDirection;
    }

    /// <summary>
    /// sets <p.moveDirection> to continuously face towards <target.transform.position>, for <homingDuration> seconds.
    /// </summary>
    public static IEnumerator HomeInOn(this Projectile p, Actor target, float homingDuration, float smoothTime = 0.5f, float delay = 0f)
    {
        if (target == null || homingDuration <= 0f) yield break;
        if (delay > 0f) yield return WaitForSeconds(delay);

        float currentTime = 0f;
        Vector3 vel = p.moveDirection;

        while (currentTime < homingDuration && target != null)
        {
            Vector3 difference = target.transform.position - p.transform.position;
            p.moveDirection = Vector3.SmoothDamp(p.moveDirection, difference, ref vel, smoothTime);

            currentTime += Time.deltaTime;
            yield return EndOfFrame;
        }
    }

    /// <summary>
    /// modified version of LookAt() that gradually rotates <p> over 
    /// </summary>
    public static IEnumerator GraduallyLookAt(this Projectile p, Vector3 target, float turnTime, float delay = 0f)
    {
        if (turnTime <= 0f) yield break;
        if (delay > 0f) yield return WaitForSeconds(delay);

        float currentTime = 0f;
        Vector3 startDirection = p.moveDirection;

        while (currentTime < turnTime)
        {
            float theta = startDirection.GetRotationDifference(target);
            float actualTheta = Mathf.Lerp(0f, theta, currentTime / turnTime);

            Vector3 newDirection = startDirection.RotateVectorBy(actualTheta).normalized;
            p.moveDirection = newDirection;

            currentTime += Time.deltaTime;
            yield return EndOfFrame;
        }
    }

    #endregion

    #region Helpers/Extensions

    /// <summary>
    /// (unused) returns the angle (in degrees) that the line created by <pos1> and <pos2> subtends from (0, 0).
    /// </summary>
    public static float GetRotationDifference(this Vector3 pos1, Vector3 pos2)
    {
        Vector3 distance = pos2 - pos1;
        return Mathf.Atan2(-distance.x, distance.y) * Mathf.Rad2Deg;
    }

    /// <summary>
    /// rotates <v> anticlockwise by <theta> degrees along the x-y plane.
    /// </summary>
    public static Vector3 RotateVectorBy(this Vector3 v, float theta)
    {
        Vector3 _v = v;
        float theta_r = theta * Mathf.Deg2Rad;

        v.x = (Mathf.Cos(theta_r) * _v.x) - (Mathf.Sin(theta_r) * _v.y);
        v.y = (Mathf.Sin(theta_r) * _v.x) + (Mathf.Cos(theta_r) * _v.y);

        return v;
    }

    /// <summary>
    /// overload of RotateVectorBy() which allows <v> to be passed by reference instead
    /// </summary>
    public static void RotateVectorBy(ref Vector3 v, float theta)
    {
        Vector3 _v = v;
        float theta_r = theta * Mathf.Deg2Rad;

        v.x = (Mathf.Cos(theta_r) * _v.x) - (Mathf.Sin(theta_r) * _v.y);
        v.y = (Mathf.Sin(theta_r) * _v.x) + (Mathf.Cos(theta_r) * _v.y);
    }

    /// <summary>
    /// (remove later?) Debug.Log shortcut. 
    /// </summary>
    static void print(object message) { Debug.Log(message); }

    #endregion
}