using System.Collections;

public class AquariusBullet42 : BossBullet
{
    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;
        yield return this.LerpSpeed(4f, endSpeed, 1f);
    }
}