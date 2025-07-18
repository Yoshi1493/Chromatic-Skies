using System.Collections;
using UnityEngine;

public class AquariusCommonMovement04 : CommonEnemyMovement
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

        yield return LeaveScene();
    }
}