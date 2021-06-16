using System.Collections;
using UnityEngine;

public class AriesAttack1 : BulletSystem
{
    EnemyBulletPool objectPool;

    void Awake()
    {
        objectPool = FindObjectOfType<EnemyBulletPool>();
    }

    public override void Play()
    {
        throw new System.NotImplementedException();
    }
}