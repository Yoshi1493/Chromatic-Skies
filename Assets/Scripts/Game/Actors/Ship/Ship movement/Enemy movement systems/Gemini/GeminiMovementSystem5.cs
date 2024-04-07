using System.Collections;
using static CoroutineHelper;

public class GeminiMovementSystem5 : EnemyMovement
{
    const int MovesBeforeTeleport = 3;

    protected override IEnumerator Move()
    {
        var ownerShip = GetComponentInParent<Enemy>();
        var twin = FindObjectOfType<GeminiBullet50>();

        yield return this.MoveToRandomPosition(2f, 4f, 4f);

        while (enabled)
        {
            for (int i = 0; i < MovesBeforeTeleport; i++)
            {
                yield return WaitForSeconds(3f);
                yield return this.MoveToRandomPosition(1f, 0.5f, 1f);
            }

            ownerShip.transform.position = twin.transform.position;
        }
    }
}