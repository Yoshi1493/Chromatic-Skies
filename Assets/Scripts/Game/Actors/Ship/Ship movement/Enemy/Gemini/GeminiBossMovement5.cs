using System.Collections;
using static CoroutineHelper;

public class GeminiBossMovement5 : BossMovement
{
    Boss ownerShip;
    GeminiBullet50 twin;

    const int MaxMovesBeforeTeleport = 3;
    int movesBeforeTeleport;

    protected override void OnEnable()
    {
        base.OnEnable();

        ownerShip = GetComponentInParent<Boss>();
        movesBeforeTeleport = MaxMovesBeforeTeleport;
    }

    protected override IEnumerator Move()
    {
        twin = FindAnyObjectByType<GeminiBullet50>();

        yield return parentShip.MoveToRandomPosition(2f, 3f, 4f);
        yield return WaitForSeconds(2f);

        while (enabled)
        {
            yield return WaitForSeconds(1f);

            yield return parentShip.MoveToRandomPosition(1f, 1f, 2f);
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