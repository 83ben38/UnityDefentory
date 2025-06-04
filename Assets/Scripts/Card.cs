using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Card", order = 1)]
public class Card : ScriptableObject
{
    public int numLeft = 1;
    public GameObject prefab;
    public Dictionary<Shape.Type, int> cost;
    public List<Shape.Type> serializedCost;
    public Sprite display;
    private void Awake()
    {
        if (serializedCost != null)
        {
            foreach (Shape.Type type in serializedCost)
            {
                if (!cost.ContainsKey(type))
                {
                    cost[type] = 0;
                }

                cost[type]++;
            }
        }
    }
}
