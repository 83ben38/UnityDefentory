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
    private void Start()
    {
        inventory = new Dictionary<Shape.Type, int>();
        timeLeft = cooldown;
    }
    private void FixedUpdate()
    {
        timeLeft -= Time.fixedDeltaTime;
        if (!(timeLeft <= 0)) return;
        Enemy target = Enemy.enemies.FirstOrDefault(t => (t.transform.position - transform.position).magnitude <= range);

        if (!target) return;
        foreach (var t in possibleAmmo.Where(t => inventory.ContainsKey(t)).Where(t => inventory[t] >= numAmmoUsed))
        {
            inventory[t] -= numAmmoUsed;
            timeLeft = cooldown;
            GameObject go = Instantiate(projectilePrefab);
            Projectile proj = go.GetComponent<Projectile>();
            go.transform.position = transform.position;
            proj.setPower(Shape.getPower(t));
            proj.target = target;
            break;
        }
    }
}
