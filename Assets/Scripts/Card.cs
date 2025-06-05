using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Card", order = 1)]
public class Card : ScriptableObject
{
    public GameObject prefab;
    public Shape.Type costType;
    public int costAmount;
    public Sprite display;
}
