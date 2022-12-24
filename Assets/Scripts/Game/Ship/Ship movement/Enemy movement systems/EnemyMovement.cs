using System.Collections;
using static CoroutineHelper;

public abstract class EnemyMovement : ShipMovement
{
    protected IEnumerator moveCoroutine;

    protected virtual IEnumerator Move()
    {
        yield return WaitForSeconds(1f);
    }

    void OnEnable()
    {
        StartCoroutine(this.ReturnToOriginalPosition());
        
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }

        moveCoroutine = Move();
        StartCoroutine(moveCoroutine);
    }

    protected override void OnLoseLife()
    {
        StopAllCoroutines();
        StartCoroutine(this.ReturnToOriginalPosition());
    }
}