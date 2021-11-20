using System.Collections;

public class PiscesBullet2 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        MoveSpeed = 4f;
        yield return null;
    }
}