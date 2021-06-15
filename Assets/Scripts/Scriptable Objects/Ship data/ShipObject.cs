using System.Collections.Generic;
using UnityEngine;

public abstract class ShipObject : ScriptableObject
{
    [Header("General")]
    public Sprite sprite;
    public StringObject shipName;

    [Header("Stats")]
    public IntReference Health;
    public IntReference Power;
    public IntReference Defense;

    public FloatReference MovementSpeed;
    public FloatReference ShootingSpeed;

    [Header("Bullets")]
    public List<GameObject> bullets = new List<GameObject>();
}