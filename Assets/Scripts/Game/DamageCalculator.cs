using UnityEngine;

public static class DamageCalculator
{
    public static int CalculateDamage(int atk, int def, float speed)
    {
        float trueAtk = atk * speed;
        float trueDef = 100f / (100f + def);
        float dmg = trueAtk * trueDef;
        Debug.Log($"({atk} * {speed}) * (100 / (100 + {def}))");
        int trueDmg = Mathf.Max(1, Mathf.CeilToInt(dmg));
        return trueDmg;
    }
}