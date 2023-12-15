using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public static class ProjectileBehaviour
{
    static AnimationCurve EaseInOutCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    /// <summary>
    /// lerps <p.MoveSpeed> from <startSpeed> to <endSpeed>, in <lerpTime> seconds.
    /// </summary>
    public static IEnumerator LerpSpeed(this Bullet p, float startSpeed, float endSpeed, float lerpTime, float delay = 0f)
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
    public static IEnumerator LerpDirection(this Bullet p, Vector3 endDirection, float lerpTime, float delay = 0f)
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
        int directionMultiplier = clockwise ? -1 : 1;

        if (delay > 0f) yield return WaitForSeconds(delay);

        if (rotateDuration <= 0f)
        {
            if (rotateDuration == 0f) { RotateVectorBy(ref p.moveDirection, degrees * directionMultiplier); }
            yield break;
        }

        float currentTime = 0f;

        while (currentTime < rotateDuration)
        {
            float degreesPerFrame = degrees * directionMultiplier / rotateDuration * Time.deltaTime;

            if (p is Laser)
            {
                p.transform.Rotate(degreesPerFrame * Vector3.forward);
            }
            else
            {
                RotateVectorBy(ref p.moveDirection, degreesPerFrame);
            }

            currentTime += Time.deltaTime;
            yield return EndOfFrame;
        }
    }

    /// <summary>
    /// rotates <p> around <target.transform.position> by setting <p.MoveSpeed> and <p.moveDirection>.
    /// rotates by <degreesPerSecond> degrees per second, for <rotateDuration> seconds
    /// </summary>
    public static IEnumerator RotateAround(this Bullet p, Actor target, float rotateDuration, float degreesPerSecond, bool clockwise = true, float delay = 0f)
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
    }

    /// <summary>
    /// overload of RotateAround() that takes in a Vector3 to rotate around, instead of an Actor.
    /// </summary>
    public static IEnumerator RotateAround(this Bullet p, Vector3 targetPosition, float rotateDuration, float degreesPerSecond, bool clockwise = true, float delay = 0f)
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
    }

    public static IEnumerator TransformRotateAround(this Bullet p, Vector3 targetPosition, float rotateDuration, float degreesPerSecond, bool clockwise = true, float delay = 0f)
    {
        if (rotateDuration <= 0) yield break;
        if (delay > 0f) yield return WaitForSeconds(delay);

        float currentTime = 0f;
        int rotationDirection = clockwise ? -1 : 1;

        Vector3 direction = p.transform.position - targetPosition;

        while (currentTime < rotateDuration)
        {
            p.transform.RotateAround(targetPosition, Vector3.forward, degreesPerSecond * rotationDirection * Time.deltaTime);

            currentTime += Time.deltaTime;
            yield return EndOfFrame;
        }

        p.transform.position = direction.RotateVectorBy(degreesPerSecond * rotateDuration * rotationDirection) + targetPosition;
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
    /// modified version of LookAt() that gradually rotates <p> over <turnDuration> seconds
    /// </summary>
    public static IEnumerator GraduallyLookAt(this Projectile p, Vector3 target, float turnDuration, float delay = 0f)
    {
        if (turnDuration <= 0f) yield break;
        if (delay > 0f) yield return WaitForSeconds(delay);

        float currentTime = 0f;
        Vector3 startDirection = p.moveDirection;
        Vector3 startPosition = p.transform.position;
        Vector3 endDirection = target - startPosition;

        while (currentTime < turnDuration)
        {
            float lerpProgress = EaseInOutCurve.Evaluate(currentTime / turnDuration);
            Vector3 newDirection = Vector3.Slerp(startDirection, endDirection, lerpProgress);

            p.moveDirection = newDirection;

            currentTime += Time.deltaTime;
            yield return EndOfFrame;
        }
    }

    /// <summary>
    /// translates <p> to <targetPos> over <moveDuration> seconds.
    /// </summary>
    public static IEnumerator MoveTo(this Projectile p, Vector3 endPosition, float moveDuration)
    {
        if (moveDuration < 0f) yield break;
        if (moveDuration == 0f) p.transform.position = endPosition;

        float currentTime = 0f;
        Vector3 startPosition = p.transform.position;

        while (currentTime < moveDuration)
        {
            float lerpProgress = EaseInOutCurve.Evaluate(currentTime / moveDuration);

            p.transform.position = Vector3.Lerp(startPosition, endPosition, lerpProgress);

            currentTime += Time.deltaTime;
            yield return EndOfFrame;
        }

        p.transform.position = endPosition;
    }

    /// <summary>
    /// (remove later?) Debug.Log shortcut. 
    /// </summary>
    static void print(object message) { Debug.Log(message); }
}