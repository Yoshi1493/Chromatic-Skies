using System.Collections;

public abstract class EnemyMovement : ShipMovement
{
    protected abstract IEnumerator Move();

    void OnEnable()
    {
        StartCoroutine(this.ReturnToOriginalPosition());
    }

    protected override void OnLoseLife()
    {
        StopAllCoroutines();
        StartCoroutine(this.ReturnToOriginalPosition());
    }
}