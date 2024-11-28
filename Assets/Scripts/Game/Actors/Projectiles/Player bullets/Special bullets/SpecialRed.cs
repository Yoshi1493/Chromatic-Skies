using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class SpecialRed : PlayerBullet, ISpecialBullet
{
    IEnumerator ISpecialBullet.Move()
    {
        yield return null;
    }
}