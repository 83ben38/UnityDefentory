using System.Collections.Generic;
using UnityEngine;

public class LaserProjectile : Projectile
{
    public float damage;
    public float lifetime;
    public override void setPower(int power)
    {
        damage *= power;
    }

    protected float angle;
    private void Start()
    {
        Vector2 direction = target.transform.position - transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle -= 90;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        time = 0;
    }

    protected float time;
    protected List<Enemy> alreadyHit = new List<Enemy>();
    private void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
        if (time >= lifetime)
        {
            Destroy(gameObject);
            return;
        }
        foreach (Enemy enemy in Enemy.enemies)
        {
            if (!alreadyHit.Contains(enemy))
            {
                Vector2 targetPos = enemy.transform.position;


                Vector2 lineDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad),
                    Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;


                Vector2 toTarget = targetPos - (Vector2)transform.position;


                float distanceToLine = Mathf.Abs(Vector2.Dot(toTarget, new Vector2(-lineDirection.y, lineDirection.x)));
                if (distanceToLine < 0.2f + enemy.transform.localScale.x)
                {
                    doDamage(enemy);
                }

            }
        }
    }

    public override void doDamage(Enemy e)
    {
        e.takeDamage(damage, "laser");
    }
}
