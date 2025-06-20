using System.Collections.Generic;
using UnityEngine;

public class ExplodyProjectile : HomingProjectile
{
    public float explosionLifetime;

    protected bool exploding = false;
    private new void FixedUpdate()
    {
        if (!exploding)
        {
            base.FixedUpdate();
        }
        else
        {
            time += Time.fixedDeltaTime;
            if (time >= explosionLifetime)
            {
                Destroy(gameObject);
                return;
            }

            transform.localScale = new Vector3(0.1f * (1 + time*2f),0.1f * (1 + time*2f));
            foreach (Enemy enemy in Enemy.enemies)
            {
                if (!alreadyHit.Contains(enemy))
                {
                    if ((enemy.transform.position - transform.position).magnitude <= 0.1f * (1 + time) + enemy.transform.localScale.x)
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
    }

    public override void doDamage(Enemy e)
    {
        if (!exploding)
        {
            exploding = true;
            time = 0;
        }
        e.takeDamage(damage, "exploding");
    }
}
