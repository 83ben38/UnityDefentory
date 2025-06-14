using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
   public static UpgradeManager instance;
   private List<UpgradeCard.Upgrade> unlocked = new();
   public List<UpgradeCard> notUnlocked;
   private void Awake()
   {
      instance = this;
   }

   public bool isUpgradeAvailable(UpgradeCard.Upgrade upgrade)
   {
      return unlocked.Contains(upgrade);
   }

   public void unlock(UpgradeCard.Upgrade upgrade)
   {
      notUnlocked.RemoveAll(x => x.upgrade == upgrade);
      unlocked.Add(upgrade);
   }
}
