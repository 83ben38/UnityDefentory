using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
   public Dictionary<Vector2Int, Tile> grid = new();

   public static GridController instance;

   public Tile[] startingVortexes;

   public void addToGrid(Tile tile)
   {
      grid[tile.location] = tile;
   }
   public void Awake()
   {
      instance = this;
   }

   private void Start()
   {
      for (int i = 0; i < startingVortexes.Length; i++)
      {
         addToGrid(startingVortexes[i]);
      }
   }
}
