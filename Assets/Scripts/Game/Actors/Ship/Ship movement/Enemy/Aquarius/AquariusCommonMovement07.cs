using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusCommonMovement07 : CommonEnemyMovement
{
    [SerializeField] int rotationDirection;

    protected override IEnumerator Move()
    {
        float yPos = parentShip.transform.position.y;
        float rotationAmount = 30f;

        yield return parentShip.TranslateAround(parentShip.transform.position + (yPos * 2f * rotationDirection * Vector3.right), rotationAmount * rotationDirection, 2f);
        yield return parentShip.TranslateAround(parentShip.transform.position + new Vector3(yPos * 2f * Mathf.Cos(rotationAmount * Mathf.Deg2Rad) * -rotationDirection, -yPos), rotationAmount * -rotationDirection, 2f);

        yield return WaitForSeconds(1f);
        yield return LeaveScene();
    }
}