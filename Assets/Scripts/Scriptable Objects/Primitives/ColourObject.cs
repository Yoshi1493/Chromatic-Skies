using UnityEngine;

[CreateAssetMenu(fileName = "New Colour", menuName = "Scriptable Object/Data Type/Colour")]
public class ColourObject : ScriptableObject
{
    [ColorUsage(true, true)]
    public Color value;
}