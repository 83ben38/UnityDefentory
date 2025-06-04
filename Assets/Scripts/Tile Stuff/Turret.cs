using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    
    public Dictionary<Shape.Type, int> inventory;
    
    private void Start()
    {
        inventory = new Dictionary<Shape.Type, int>();
    }

}
