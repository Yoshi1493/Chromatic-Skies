using System.Collections;
using UnityEngine;
using static CoroutineHelper;

//helper class to handle ship movement
public static class ShipMovementHelper
{
    #region Movement behaviour

    static readonly AnimationCurve moveInterpolation = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    public static readonly Vector3 bossSpawnPosition = new(0f, 2.5f, 0f);
    public static readonly Vector3 playerSpawnPosition = new(0f, -2f, 0f);

    /// <summary>
    /// translates <ship> to <endPosition> over <moveDuration> seconds, along a sigmoid (smoothstep) curve.
    /// </summary>
    public static IEnumerator MoveTo<TShip>(this ShipMovement<TShip> ship, Vector3 endPosition, float moveDuration, float delay = 0f)
        where TShip : Ship
    {
        if (delay > 0) yield return WaitForSeconds(delay);
        if (moveDuration <= 0f) ship.parentShip.transform.position = endPosition;

        Vector3 startPosition = ship.transform.position;
        ship.moveDirection = endPosition - startPosition;
        float maxSpeed = ship.moveDirection.magnitude * (2f / moveDuration);

        float currentTime = 0f;
        while (currentTime < moveDuration / 2f)
        {
            ship.currentSpeed = Mathf.Lerp(0f, maxSpeed, moveInterpolation.Evaluate(currentTime * 2f / moveDuration));

            yield return null;
            currentTime += Time.deltaTime;
        }

        while (currentTime < moveDuration)
        {
            ship.currentSpeed = Mathf.Lerp(maxSpeed, 0f, moveInterpolation.Evaluate((currentTime * 2f - moveDuration) / moveDuration));

            yield return null;
            currentTime += Time.deltaTime;
        }

        ship.parentShip.transform.position = endPosition;
        ship.moveDirection = Vector3.zero;
        ship.currentSpeed = 0f;
    }

    public static IEnumerator MoveFromTo<TShip>(this ShipMovement<TShip> ship, Vector3 startPosition, Vector3 endPosition, float moveDuration, float delay = 0f)
        where TShip : Ship
    {
        if (delay > 0) yield return WaitForSeconds(delay);

        ship.parentShip.transform.position = startPosition;
        yield return ship.MoveTo(endPosition, moveDuration);
    }

    /// <summary>
    /// translates <ship> to <GetRandomPositionWithinBounds()> over <moveDuration> seconds.
    /// </summary>
    public static IEnumerator MoveToRandomPosition(this BossMovement ship, float moveDuration, float minDeltaMagnitude = 2f, float maxDeltaMagnitude = 4f, float delay = 0f)
    {
        if (minDeltaMagnitude > maxDeltaMagnitude) yield break;
        if (delay > 0) yield return WaitForSeconds(delay);

        Vector3 endPosition = ship.transform.position.GetRandomPositionWithinBounds(ship.shipData.boundaryLayer, minDeltaMagnitude, maxDeltaMagnitude);
        yield return ship.MoveTo(endPosition, moveDuration);
    }

    /// <summary>
    /// linearly translates <ship> to <endPosition> over <moveDuration> seconds.
    /// </summary>
    public static IEnumerator MoveToLinear<TShip>(this ShipMovement<TShip> ship, Vector3 endPosition, float moveDuration, float delay = 0f)
        where TShip : Ship
    {
        if (delay > 0) yield return WaitForSeconds(delay);

        Vector3 newMoveDirection = endPosition - ship.transform.position;
        float newMoveSpeed = newMoveDirection.magnitude / moveDuration;

        ship.moveDirection = newMoveDirection;
        ship.currentSpeed = newMoveSpeed;

        yield return WaitForSeconds(moveDuration);

        ship.parentShip.transform.position = endPosition;
        ship.moveDirection = Vector3.zero;
        ship.currentSpeed = 0f;
    }

    /// <summary>
    /// translates <ship> by setting ship's move direction and move speed, for <moveDuration> seconds.
    /// moves relatively as opposed to setting a fixed end position.
    /// </summary>
    public static IEnumerator MoveRelative<TShip>(this ShipMovement<TShip> ship, Vector3 moveDirection, float moveSpeed, float moveDuration, float delay = 0f)
        where TShip : Ship
    {
        if (moveDirection == Vector3.zero || moveSpeed == 0 || moveDuration <= 0f) yield break;
        if (delay > 0) yield return WaitForSeconds(delay);

        Vector3 normalizedDirection = moveDirection.normalized;
        Vector3 startPosition = ship.parentShip.transform.position;
        Vector3 endPosition = (moveSpeed * moveDuration * normalizedDirection) + startPosition;

        ship.moveDirection = normalizedDirection;
        ship.currentSpeed = moveSpeed;

        yield return WaitForSeconds(moveDuration);

        ship.parentShip.transform.position = endPosition;
        ship.moveDirection = Vector3.zero;
        ship.currentSpeed = 0f;
    }

    /// <summary>
    /// translates <ship> anticlockwise around <point> by <degrees> degrees over <moveDuration> seconds.
    /// </summary>
    public static IEnumerator TranslateAround<TShip>(this ShipMovement<TShip> ship, Vector3 point, float degrees, float moveDuration, float delay = 0f)
        where TShip : Ship
    {
        if (degrees == 0f || moveDuration <= 0f) yield break;
        if (delay > 0) yield return WaitForSeconds(delay);

        Vector3 startDirection = ship.transform.position - point;
        Vector3 endPosition = startDirection.RotateVectorBy(degrees) + point;

        float currentTime = 0f;
        while (currentTime < moveDuration)
        {
            float r = Mathf.Lerp(0f, degrees, currentTime / moveDuration);
            ship.moveDirection = startDirection.RotateVectorBy(90f).RotateVectorBy(r);
            ship.currentSpeed = ship.moveDirection.magnitude * degrees * Mathf.Deg2Rad / moveDuration;

            yield return null;
            currentTime += Time.deltaTime;
        }

        ship.parentShip.transform.position = endPosition;
        ship.moveDirection = Vector3.zero;
        ship.currentSpeed = 0f;
    }

    /// <summary>
    /// returns a random position that is <minSqrMagDelta> to <maxSqrMagDelta> units away from <currentPostion>, within <bounds>
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

    public static IEnumerator ReturnToOriginalPosition<TShip>(this ShipMovement<TShip> ship, float moveDuration = 1f, float delay = 0f)
        where TShip : Ship
    {
        if (ship.parentShip is Boss)
        {
            yield return ship.MoveTo(bossSpawnPosition, moveDuration, delay);
        }

        if (ship.parentShip is Player)
        {
            yield return ship.MoveTo(playerSpawnPosition, moveDuration, delay);
        }
    }
    #endregion

    #region Debug

    /// <summary>
    /// (remove later?) Debug.Log shortcut. 
    /// </summary>
    static void print(object message) { Debug.Log(message); }

    #endregion
}