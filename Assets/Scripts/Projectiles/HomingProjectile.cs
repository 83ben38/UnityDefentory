using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : Projectile
{
    public float damage;
    public int pierce;
    public float lifetime;
    public float moveSpeed;
    public override void setPower(float power, Tile t)
    {
        damage += t.getDamageAddition();
        pierce += t.getPierceAddition();
        damage *= power;
    }

    private void Start()
    {
        pierceLeft = pierce;
        time = 0;
    }

    protected Vector2 moveDirection = new Vector2(0, 0);
    protected float time;
    protected float pierceLeft;
    protected List<Enemy> alreadyHit = new List<Enemy>();
    protected void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
        if (time >= lifetime)
        {
            Destroy(gameObject);
            return;
        }
        if (target)
        {
            moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
        }
        transform.Translate(moveDirection * Time.fixedDeltaTime);
        foreach (Enemy enemy in Enemy.enemies)
        {
            if (!alreadyHit.Contains(enemy))
            {
                if ((enemy.transform.position - transform.position).magnitude <= 0.1f + enemy.transform.localScale.x)
                {
                    if (enemy == target)
                    {
                        target = null;
                    }
                    doDamage(enemy);
                    pierceLeft--;
                    if (pierceLeft == 0)
                    {
                        Destroy(gameObject);
                        return;
                    }
                    alreadyHit.Add(enemy);
                }
            }
        }
    }

    public override void doDamage(Enemy e)
    {
        e.takeDamage(damage, "homing");
    }
}
