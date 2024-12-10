using System;
using UnityEngine;

[Serializable]
public class FloatReference
{
    [SerializeField] bool useConstant;

    [SerializeField] float ConstantValue;
    public FloatObject Variable;

    public float Value
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