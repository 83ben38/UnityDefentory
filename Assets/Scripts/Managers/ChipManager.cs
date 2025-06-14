using System;
using System.Collections.Generic;
using UnityEngine;

public class ChipManager : MonoBehaviour
{
   public static ChipManager instance;
   public List<ChipCard.Chip> inventory = new();
   public List<ChipCard> available;
   private void Awake()
   {
      instance = this;
   }
   
   public void get(ChipCard.Chip chip)
   {
      inventory.Add(chip);
   }
}
