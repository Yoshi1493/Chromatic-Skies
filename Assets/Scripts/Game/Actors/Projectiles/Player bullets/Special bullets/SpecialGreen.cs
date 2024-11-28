using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class SpecialGreen : PlayerBullet, ISpecialBullet
{
    IEnumerator ISpecialBullet.Move()
    {
        yield return null;
    }
}