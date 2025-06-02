using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public Dictionary<Shape.Type,int> resources = new();
    public static ResourceManager instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        
    }
}
