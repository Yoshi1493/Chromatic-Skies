using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusMovementSystem03 : CommonEnemyMovement
{
    [SerializeField] Vector2[] rotationPoints;

    protected override IEnumerator Move()
    {
        float dist = Vector2.Distance(transform.position, rotationPoints[0]);
        float theta = Mathf.Asin(2f / dist) * Mathf.Rad2Deg * 2;

        for (int i = 0; i < rotationPoints.Length; i++)
        {
            yield return parentShip.TranslateAround(rotationPoints[i], (i % 2 * 2 - 1) * theta, 1.5f);
        }

        yield return WaitForSeconds(1f);
        yield return LeaveScene();
    }
}