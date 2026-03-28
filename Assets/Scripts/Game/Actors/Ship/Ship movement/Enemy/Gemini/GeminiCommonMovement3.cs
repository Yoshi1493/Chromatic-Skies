using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class GeminiCommonMovement3 : CommonEnemyMovement
{
    Player player;

    protected override void Awake()
    {
        base.Awake();
        player = FindAnyObjectByType<Player>();
    }

    protected override IEnumerator Move()
    {
        float d = Mathf.Sign(player.transform.position.x - parentShip.transform.position.x);

        yield return parentShip.MoveRelative(Vector3.down.RotateVectorBy(d * 15f), Random.Range(3f, 5f), 1f);
        yield return WaitForSeconds(4f);

        yield return LeaveScene();
    }
}