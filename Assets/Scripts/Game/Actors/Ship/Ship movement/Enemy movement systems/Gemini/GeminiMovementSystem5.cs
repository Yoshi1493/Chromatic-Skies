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

        yield return this.MoveToRandomPosition(2f, 3f, 4f);
        yield return WaitForSeconds(2f);

        while (enabled)
        {
            yield return WaitForSeconds(1f);

            yield return this.MoveToRandomPosition(1f, 1f, 2f);
            movesBeforeTeleport--;

            yield return WaitForSeconds(1f);

            if (movesBeforeTeleport <= 0)
            {
                Teleport();
            }
        }
    }

    void Teleport()
    {
        ownerShip.transform.position = twin.transform.position;
        movesBeforeTeleport = MaxMovesBeforeTeleport;
    }
}