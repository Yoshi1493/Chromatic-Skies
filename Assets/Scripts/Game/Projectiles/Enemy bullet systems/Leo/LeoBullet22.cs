using System.Collections;
using UnityEngine;

public class LeoBullet22 : ScriptableEnemyBullet<LeoBulletSystem21, EnemyBullet>
{
    const float EndSpeed = LeoBulletSystem21.BulletSpeed;
    const int BulletCount = 12;
    const float BulletSpacing = 12f;

    protected override float MaxLifetime => 3f;

    protected override IEnumerator Move()
    {
        float startSpeed = MoveSpeed;
        float direction = Mathf.Sign(startSpeed);

        yield return this.LerpSpeed(startSpeed, 0f, 1f);

        yield return this.RotateBy(90f * direction, 0f);

        float endSpeed = Mathf.Sqrt((EndSpeed * EndSpeed) - (startSpeed * startSpeed)) * direction;
        yield return this.LerpSpeed(0f, endSpeed, 1 - 0.7071f);
        yield return this.LerpSpeed(endSpeed, 0f, 0.7071f);

        moveDirection = transform.position - ownerShip.transform.position;
    }

    public override void Destroy()
    {
        for (int i = 0; i < BulletCount; i++)
        {
            float z = i * BulletSpacing + transform.eulerAngles.z;
            Vector3 pos = transform.position;

            var bullet = SpawnBullet(3, z, pos, false);
            bullet.MoveSpeed = i * 0.25f + 1;
            bullet.Fire();
        }

        base.Destroy();
    }
}