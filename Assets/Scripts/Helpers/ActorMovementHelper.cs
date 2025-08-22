using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;
using static CameraBoundaries;

//helper class to handle Actor movement
public static class ActorMovementHelper
{
    #region Fields

    static readonly AnimationCurve moveInterpolation = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    public static readonly Vector3 bossSpawnPosition = new(0f, 2.5f, 0f);
    public static readonly Vector3 playerSpawnPosition = new(0f, -2f, 0f);

    #endregion

    #region Position checks

    public static bool IsWithinCameraBounds(this Actor actor)
    {
        return actor.transform.position.x > -ScreenHalfWidth
            && actor.transform.position.x < ScreenHalfWidth
            && actor.transform.position.y > -ScreenHalfHeight
            && actor.transform.position.y < ScreenHalfHeight;
    }

    #endregion

    #region Direct lerp methods

    /// <summary>
    /// lerps <actor.MoveSpeed> from <startSpeed> to <endSpeed>, in <duration> seconds
    /// </summary>
    public static IEnumerator LerpSpeed(this Actor actor, float startSpeed, float endSpeed, float duration, float delay = 0f)
    {
        if (duration <= 0f) yield break;
        if (delay > 0f) yield return WaitForSeconds(delay);

        actor.MoveSpeed = startSpeed;
        float currentTime = 0f;

        while (currentTime < duration)
        {
            actor.MoveSpeed = Mathf.Lerp(startSpeed, endSpeed, currentTime / duration);

            yield return null;
            currentTime += Time.deltaTime;
        }

        actor.MoveSpeed = endSpeed;
    }

    /// <summary>
    /// uses Vector3.SmoothDamp to lerp <actor.moveDirection> from current moveDirection to <endDirection>, in <duration> seconds.
    /// </summary>
    public static IEnumerator LerpDirection(this Actor actor, Vector3 endDirection, float duration, float delay = 0f)
    {
        if (duration <= 0f) yield break;
        if (delay > 0f) yield return WaitForSeconds(delay);

        Vector3 dir = actor.moveDirection;

        while (actor.moveDirection != endDirection)
        {
            actor.moveDirection = Vector3.SmoothDamp(actor.moveDirection, endDirection, ref dir, duration);
            yield return null;
        }

        actor.moveDirection = endDirection;
    }

    /// <summary>
    /// lerps <actor.SpriteRenderer.size> from current size to <endSize>, in <duration> seconds.
    /// </summary>
    public static IEnumerator LerpSize(this Actor actor, Vector2 endSize, float duration, float delay = 0f)
    {
        if (duration <= 0f) yield break;
        if (delay > 0f) yield return WaitForSeconds(delay);

        Vector2 startSize = actor.SpriteRenderer.size;
        float currentLerpTime = 0f;

        while (actor.SpriteRenderer.size != endSize)
        {
            actor.SpriteRenderer.size = Vector2.Lerp(startSize, endSize, currentLerpTime / duration);

            currentLerpTime += Time.deltaTime;
            yield return null;
        }

        actor.SpriteRenderer.size = endSize;
    }

    #endregion

    #region Ship movement methods

    /// <summary>
    /// translates <actor> to <endPosition>, over <duration> seconds, along a sigmoid (smoothstep) curve.
    /// </summary>
    public static IEnumerator MoveTo(this Actor actor, Vector3 endPosition, float duration, float delay = 0f)
    {
        if (duration <= 0f) yield break;
        if (delay > 0f) yield return WaitForSeconds(delay);

        actor.moveDirection = endPosition - actor.transform.position;
        float maxSpeed = actor.moveDirection.magnitude * (2f / duration);
        float currentTime = 0f;

        while (currentTime < duration / 2f)
        {
            actor.MoveSpeed = Mathf.Lerp(0f, maxSpeed, moveInterpolation.Evaluate(currentTime * 2f / duration));

            yield return null;
            currentTime += Time.deltaTime;
        }

        while (currentTime < duration)
        {
            actor.MoveSpeed = Mathf.Lerp(maxSpeed, 0f, moveInterpolation.Evaluate((currentTime * 2f - duration) / duration));

            yield return null;
            currentTime += Time.deltaTime;
        }

        actor.transform.position = endPosition;
        actor.moveDirection = Vector3.zero;
        actor.MoveSpeed = 0f;
    }

    /// <summary>
    /// same as MoveTo, but position is translated linearly
    /// </summary>
    public static IEnumerator MoveToLinear(this Actor actor, Vector3 endPosition, float duration, float delay = 0f)
    {
        if (duration <= 0f) yield break;
        if (delay > 0f) yield return WaitForSeconds(delay);

        Vector3 newMoveDirection = endPosition - actor.transform.position;
        float newMoveSpeed = newMoveDirection.magnitude / duration;

        actor.moveDirection = newMoveDirection;
        actor.MoveSpeed = newMoveSpeed;

        yield return WaitForSeconds(duration);

        actor.transform.position = endPosition;
        actor.moveDirection = Vector3.zero;
        actor.MoveSpeed = 0f;
    }

    /// <summary>
    /// translates <actor> based on <moveDirection> and <moveSpeed>, for <duration> seconds.
    /// moves relatively as opposed to setting a fixed end position.
    /// </summary>
    public static IEnumerator MoveRelative(this Actor actor, Vector3 moveDirection, float moveSpeed, float duration, float delay = 0f)
    {
        if (moveDirection == Vector3.zero || moveSpeed == 0 || duration <= 0f) yield break;
        if (delay > 0f) yield return WaitForSeconds(delay);

        Vector3 normalizedDirection = moveDirection.normalized;
        Vector3 endPosition = (moveSpeed * duration * normalizedDirection) + actor.transform.position;
        actor.moveDirection = normalizedDirection;
        float maxSpeed = moveSpeed * 2f;
        float currentTime = 0f;

        while (currentTime < duration / 2f)
        {
            actor.MoveSpeed = Mathf.Lerp(0f, maxSpeed, moveInterpolation.Evaluate(currentTime * 2f / duration));

            yield return null;
            currentTime += Time.deltaTime;
        }

        while (currentTime < duration)
        {
            actor.MoveSpeed = Mathf.Lerp(maxSpeed, 0f, moveInterpolation.Evaluate((currentTime * 2f - duration) / duration));

            yield return null;
            currentTime += Time.deltaTime;
        }

        actor.transform.position = endPosition;
        actor.moveDirection = Vector3.zero;
        actor.MoveSpeed = 0f;
    }

    /// <summary>
    /// same as MoveRelative, but position is translated linearly
    /// </summary>
    public static IEnumerator MoveRelativeLinear(this Actor actor, Vector3 moveDirection, float moveSpeed, float duration, float delay = 0f)
    {
        if (moveDirection == Vector3.zero || moveSpeed == 0 || duration <= 0f) yield break;
        if (delay > 0f) yield return WaitForSeconds(delay);

        Vector3 normalizedDirection = moveDirection.normalized;
        Vector3 startPosition = actor.transform.position;
        Vector3 endPosition = (moveSpeed * duration * normalizedDirection) + startPosition;

        actor.moveDirection = normalizedDirection;
        actor.MoveSpeed = moveSpeed;

        yield return WaitForSeconds(duration);

        actor.transform.position = endPosition;
        actor.moveDirection = Vector3.zero;
        actor.MoveSpeed = 0f;
    }

    /// <summary>
    /// teleports <actor> to <startPosition> first, then translates from <startPosition> to <endPosition>, over <duration> seconds.
    /// </summary>
    public static IEnumerator MoveFromTo(this Actor actor, Vector3 startPosition, Vector3 endPosition, float duration, float delay = 0f)
    {
        if (delay > 0f) yield return WaitForSeconds(delay);

        actor.transform.position = startPosition;
        yield return actor.MoveTo(endPosition, duration);
    }

    /// <summary>
    /// translates <actor> anticlockwise around <point> by <degrees> degrees over <duration> seconds.
    /// </summary>
    public static IEnumerator TranslateAround(this Actor actor, Vector3 point, float degrees, float duration, float delay = 0f)
    {
        if (degrees == 0f || duration <= 0f) yield break;
        if (delay > 0f) yield return WaitForSeconds(delay);

        Vector3 startDirection = actor.transform.position - point;
        Vector3 endPosition = startDirection.RotateVectorBy(degrees) + point;
        float currentTime = 0f;

        while (currentTime < duration)
        {
            float r = Mathf.Lerp(0f, degrees, currentTime / duration);
            actor.moveDirection = startDirection.RotateVectorBy(90f).RotateVectorBy(r);
            actor.MoveSpeed = actor.moveDirection.magnitude * degrees * Mathf.Deg2Rad / duration;

            yield return null;
            currentTime += Time.deltaTime;
        }

        actor.transform.position = endPosition;
        actor.moveDirection = Vector3.zero;
        actor.MoveSpeed = 0f;
    }

    /// <summary>
    /// translates <ship> to <GetRandomPositionWithinBounds()>, over <duration> seconds.
    /// </summary>
    public static IEnumerator MoveToRandomPosition(this Ship ship, float duration, float minDeltaMagnitude = 2f, float maxDeltaMagnitude = 4f, float delay = 0f)
    {
        if (duration <= 0f || minDeltaMagnitude > maxDeltaMagnitude) yield break;
        if (delay > 0f) yield return WaitForSeconds(delay);

        Vector3 endPosition = ship.transform.position.GetRandomPositionWithinBounds(ship.shipData.boundaryLayer, minDeltaMagnitude, maxDeltaMagnitude);
        yield return ship.MoveTo(endPosition, duration);
    }

    /// <summary>
    /// returns a random position that is <minSqrMagDelta> to <maxSqrMagDelta> units away from <currentPostion>, within <bounds>.
    /// </summary>
    public static Vector3 GetRandomPositionWithinBounds(this Vector3 currentPosition, LayerMask bounds, float minDeltaMagnitude, float maxDeltaMagnitude)
    {
        float randMagnitude;
        Vector3 randDirection;

        do
        {
            randMagnitude = Random.Range(minDeltaMagnitude, maxDeltaMagnitude);
            randDirection = Random.insideUnitCircle.normalized;
        }
        while (Physics2D.Raycast(currentPosition, randDirection, randMagnitude, bounds).collider != null);

        return currentPosition + (randMagnitude * randDirection);
    }

    /// <summary>
    /// translates <boss> back to original spawn position, over <duration> seconds.
    /// </summary>
    public static IEnumerator ReturnToOriginalPosition(this Boss boss, float duration = 1f, float delay = 0f)
    {
        yield return boss.MoveTo(bossSpawnPosition, duration, delay);
    }

    /// <summary>
    /// overload of ReturnToOriginalPosition for Player ship.
    /// </summary>
    public static IEnumerator ReturnToOriginalPosition(this Player player, float duration = 1f, float delay = 0f)
    {
        yield return player.MoveTo(bossSpawnPosition, duration, delay);
    }

    #endregion

    #region Projectile rotation methods

    /// <summary>
    /// rotates <actor.moveDirection> upon the x-y plane by <degrees> degrees, over <duration> seconds.
    /// </summary>
    public static IEnumerator RotateBy(this Actor actor, float degrees, float duration, bool clockwise = true, float delay = 0f)
    {
        int directionMultiplier = clockwise ? -1 : 1;

        if (delay > 0f) yield return WaitForSeconds(delay);

        if (duration <= 0f)
        {
            if (duration == 0f) { RotateVectorBy(ref actor.moveDirection, degrees * directionMultiplier); }
            yield break;
        }

        Vector3 originalDirection = actor.moveDirection;
        float currentTime = 0f;

        while (currentTime < duration)
        {
            float degreesPerFrame = degrees * directionMultiplier / duration * Time.deltaTime;

            if (actor is Laser)
            {
                actor.transform.Rotate(degreesPerFrame * Vector3.forward);
            }
            else
            {
                RotateVectorBy(ref actor.moveDirection, degreesPerFrame);
            }

            yield return null;
            currentTime += Time.deltaTime;
        }

        actor.moveDirection = originalDirection.RotateVectorBy(degrees * directionMultiplier);
    }

    /// <summary>
    /// rotates <actor> around <target.transform.position> by setting <actor.MoveSpeed> and <actor.moveDirection>.
    /// rotates by <degreesPerSecond> degrees per second, for <duration> seconds
    /// </summary>
    public static IEnumerator RotateAround(this Actor actor, Actor target, float duration, float degreesPerSecond, bool clockwise = true, float delay = 0f)
    {
        if (target == null || duration <= 0f) yield break;
        if (delay > 0f) yield return WaitForSeconds(delay);

        Vector3 direction = actor.transform.position - target.transform.position;
        float distance = direction.magnitude;

        int rotationDirection = clockwise ? -1 : 1;
        float currentTime = 0f;

        while (currentTime < duration)
        {
            RotateVectorBy(ref actor.moveDirection, degreesPerSecond * rotationDirection * Time.deltaTime);
            actor.MoveSpeed = distance * (degreesPerSecond / Mathf.Rad2Deg);

            yield return null;
            currentTime += Time.deltaTime;
        }
    }

    /// <summary>
    /// overload of RotateAround() that takes in a Vector3 to rotate around, instead of an Actor.
    /// </summary>
    public static IEnumerator RotateAround(this Actor actor, Vector3 targetPosition, float duration, float degreesPerSecond, bool clockwise = true, float delay = 0f)
    {
        if (targetPosition == actor.transform.position || duration <= 0f) yield break;
        if (delay > 0f) yield return WaitForSeconds(delay);

        Vector3 direction = actor.transform.position - targetPosition;
        float distance = direction.magnitude;

        int rotationDirection = clockwise ? -1 : 1;
        float currentTime = 0f;

        while (currentTime < duration)
        {
            RotateVectorBy(ref actor.moveDirection, degreesPerSecond * rotationDirection * Time.deltaTime);
            actor.MoveSpeed = distance * (degreesPerSecond / Mathf.Rad2Deg);

            yield return null;
            currentTime += Time.deltaTime;
        }
    }

    /// <summary>
    /// idk how to explain how is this different from RotateAround, but it is.
    /// </summary>
    public static IEnumerator TransformRotateAround(this Actor actor, Vector3 targetPosition, float duration, float degreesPerSecond, bool clockwise = true, float delay = 0f)
    {
        if (duration <= 0f) yield break;
        if (delay > 0f) yield return WaitForSeconds(delay);

        int rotationDirection = clockwise ? -1 : 1;
        float currentTime = 0f;

        Vector3 direction = actor.transform.position - targetPosition;

        while (currentTime < duration)
        {
            actor.transform.RotateAround(targetPosition, Vector3.forward, degreesPerSecond * rotationDirection * Time.deltaTime);
            RotateVectorBy(ref actor.moveDirection, degreesPerSecond * rotationDirection * Time.deltaTime);

            yield return null;
            currentTime += Time.deltaTime;
        }

        actor.transform.position = direction.RotateVectorBy(degreesPerSecond * duration * rotationDirection) + targetPosition;
    }

    #endregion

    #region Projectile homing methods

    /// <summary>
    /// sets <actor.moveDirection> to face towards <target.transform.position>
    /// i.e. sets <actor.moveDirection> such that if <target.transform.position> doesn't change and <actor.MoveSpeed> is positive, <actor> will eventually collide with <target>.
    /// </summary>
    public static Vector3 LookAt(this Actor actor, Actor target)
    {
        if (target == null || target == actor ) return actor.moveDirection;

        var newMoveDirection = target.transform.position - actor.transform.position;
        newMoveDirection.z = 0f;

        if (newMoveDirection != Vector3.zero)
        {
            actor.moveDirection = newMoveDirection;
            return actor.moveDirection;
        }
        else
        {
            return actor.moveDirection;
        }
    }

    /// <summary>
    /// overload of LookAt() that makes <actor> face towards a given Vector3 in world space, instead of an Actor.
    /// </summary>
    public static Vector3 LookAt(this Actor actor, Vector3 targetPos)
    {
        if (targetPos == null) return actor.moveDirection;

        var newMoveDirection = targetPos - actor.transform.position;
        newMoveDirection.z = 0f;

        if (newMoveDirection != Vector3.zero)
        {
            actor.moveDirection = newMoveDirection;
            return actor.moveDirection;
        }
        else
        {
            return actor.moveDirection;
        }
    }

    /// <summary>
    /// sets <actor.moveDirection> to gradually rotate towards <target.transform.position>, over <duration> seconds.
    /// </summary>
    public static IEnumerator HomeInOn(this Actor actor, Actor target, float duration, float smoothTime = 0.5f, float delay = 0f)
    {
        if (target == null || duration <= 0f) yield break;
        if (delay > 0f) yield return WaitForSeconds(delay);

        Vector3 dir = actor.moveDirection;
        float currentTime = 0f;

        while (currentTime < duration && target != null)
        {
            Vector3 difference = target.transform.position - actor.transform.position;
            actor.moveDirection = Vector3.SmoothDamp(actor.moveDirection, difference, ref dir, smoothTime);

            yield return null;
            currentTime += Time.deltaTime;
        }
    }

    /// <summary>
    /// modified version of LookAt() that gradually rotates <actor> over <duration> seconds.
    /// </summary>
    public static IEnumerator GraduallyLookAt(this Actor actor, Vector3 target, float duration, float delay = 0f)
    {
        if (duration <= 0f) yield break;
        if (delay > 0f) yield return WaitForSeconds(delay);

        Vector3 startDirection = actor.moveDirection;
        Vector3 startPosition = actor.transform.position;
        Vector3 endDirection = target - startPosition;
        float currentTime = 0f;

        while (currentTime < duration)
        {
            float lerpProgress = moveInterpolation.Evaluate(currentTime / duration);
            Vector3 newDirection = Vector3.Slerp(startDirection, endDirection, lerpProgress);

            actor.moveDirection = newDirection;

            yield return null;
            currentTime += Time.deltaTime;
        }
    }

    #endregion

    #region Debug

    /// <summary>
    /// (remove later?) Debug.Log shortcut
    /// </summary>
    static void print(object message) { Debug.Log(message); }

    #endregion
}