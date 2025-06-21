using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Turret : MonoBehaviour
{
    
    public Dictionary<Shape.Type, int> inventory;
    public List<Shape.Type> possibleAmmo;
    public int numAmmoUsed;
    public GameObject projectilePrefab;
    public float cooldown;
    private float timeLeft;
    public float range;
    public Tile tile;
    private void Start()
    {
        inventory = new Dictionary<Shape.Type, int>();
        timeLeft = cooldown;
        tile = GetComponentInParent<Tile>();
    }
    private void FixedUpdate()
    {
        timeLeft -= Time.fixedDeltaTime*tile.getSpeedMultiplier();
        if (!(timeLeft <= 0)) return;
        Enemy target = Enemy.enemies.FirstOrDefault(t => (t.transform.position - transform.position).magnitude <= range+tile.getRangeAddition());

        if (!target) return;
        foreach (var t in possibleAmmo.Where(t => inventory.ContainsKey(t)).Where(t => inventory[t] >= Mathf.Min(numAmmoUsed-tile.getPriceReduction(t),1)))
        {
            inventory[t] -= Mathf.Min(numAmmoUsed-tile.getPriceReduction(t),1);
            timeLeft = cooldown;
            GameObject go = Instantiate(projectilePrefab);
            Projectile proj = go.GetComponent<Projectile>();
            go.transform.position = transform.position;
            proj.setPower((Shape.getPower(t)+tile.getPowerAddition())*tile.getPowerMultiplier(), tile);
            proj.target = target;
            break;
        }
    }
}
