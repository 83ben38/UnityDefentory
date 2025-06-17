using System;
using System.Collections.Generic;
using UnityEngine;

public class ChipManager : MonoBehaviour
{
   public static ChipManager instance;
   public List<ChipCard> inventory = new();
   public List<ChipCard> available;
   private void Awake()
   {
      instance = this;
   }
   
   public void get(ChipCard chip)
   {
      inventory.Add(chip);
   }
}
