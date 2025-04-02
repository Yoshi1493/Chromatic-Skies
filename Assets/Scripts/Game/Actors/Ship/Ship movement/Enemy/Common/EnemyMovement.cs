using System.Collections;

public abstract class EnemyMovement : ShipMovement<Enemy>
{
    protected IEnumerator moveCoroutine;
    protected abstract IEnumerator Move();

    void OnEnable()
    {
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
    }
}