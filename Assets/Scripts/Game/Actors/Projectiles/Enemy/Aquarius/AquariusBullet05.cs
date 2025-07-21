using System.Collections;
using static CoroutineHelper;

public class AquariusBullet05 : MinibossBullet
{
    protected override IEnumerator Move()
    {
        float startSpeed = MoveSpeed;

        yield return this.LerpSpeed(startSpeed, 0f, 0.5f);
        yield return WaitForSeconds(0.5f);
        yield return this.LerpSpeed(0f, -2.5f, 2f);
    }
}