using System;
using UnityEngine;

[Serializable]
public class IntReference
{
    [SerializeField] bool useConstant;

    [SerializeField] int ConstantValue;
    public IntObject Variable;

    public int Value
    {
        get => useConstant ? ConstantValue : Variable.value;
        set
        {
            if (useConstant)
            {
                ConstantValue = value;
            }
            else
            {
                Variable.value = value;
            }
        }
    }
}