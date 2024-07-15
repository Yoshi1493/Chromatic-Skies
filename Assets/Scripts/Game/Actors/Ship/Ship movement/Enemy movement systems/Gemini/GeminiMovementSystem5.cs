using System.Collections;
using static CoroutineHelper;

public class GeminiMovementSystem5 : EnemyMovement
{
    Enemy ownerShip;
    GeminiBullet50 twin;

    const int MaxMovesBeforeTeleport = 3;
    int movesBeforeTeleport;

    protected override void OnEnable()
    {
        base.OnEnable();

        ownerShip = GetComponentInParent<Enemy>();
        movesBeforeTeleport = MaxMovesBeforeTeleport;
    }

    protected override IEnumerator Move()
    {
        twin = FindObjectOfType<GeminiBullet50>();

        yield return this.MoveToRandomPosition(2f, 4f, 4f);

        while (enabled)
        {
            yield return WaitUntil(() => movesBeforeTeleport > 0);

            yield return WaitForSeconds(2.5f);
            yield return this.MoveToRandomPosition(1f, 0.5f, 1f);
            movesBeforeTeleport--;
        }
    }

    public void Teleport()
    {
        ownerShip.transform.position = twin.transform.position;
        movesBeforeTeleport = MaxMovesBeforeTeleport;
    }
}