using UnityEngine;
public abstract class Card : ScriptableObject
{
    public Sprite display;
    public string name;
    [TextArea]
    public string description;
}
