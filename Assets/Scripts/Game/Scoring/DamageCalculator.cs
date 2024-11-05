using UnityEngine;

public static class DamageCalculator
{
    public static int CalculateDamage(int atk, int def, float speed)
    {
        float trueSpd = Mathf.Max(Mathf.Abs(speed), 1f);
        float trueAtk = atk * trueSpd;
        float trueDef = 100f / (100f + def);
        float dmg = trueAtk * trueDef;
        //Debug.Log($"({atk} * {trueSpd}) * (100 / (100 + {def}))");
        int trueDmg = Mathf.Max(Mathf.CeilToInt(dmg), 1);
        return trueDmg;
    }
}