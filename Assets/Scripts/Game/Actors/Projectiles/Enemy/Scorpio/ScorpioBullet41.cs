using System.Collections;

public class ScorpioBullet41 : BossBullet
{
    protected override IEnumerator Move()
    {
        float startSpeed = MoveSpeed;
        yield return this.LerpSpeed(startSpeed, 0f, 1f);
        yield return this.LerpSpeed(0f, startSpeed, 3f);
    }
}