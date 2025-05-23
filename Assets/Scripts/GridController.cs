using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
   public Dictionary<Vector2, Tile> grid = new();

   public static GridController instance;

   public Tile startingVortex;
   public void Awake()
   {
      instance = this;
   }

   private void Start()
   {
      grid[startingVortex.location] = startingVortex;
   }
}
