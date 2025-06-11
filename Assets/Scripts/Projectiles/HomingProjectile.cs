using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : Projectile
{
    public float damage;
    public int pierce;
    public float lifetime;
    public float moveSpeed;
    public override void setPower(int power)
    {
        damage *= power;
    }

    private void Start()
    {
        pierceLeft = pierce;
        time = 0;
    }

    private Vector2 moveDirection = new Vector2(0, 0);
    private float time;
    private float pierceLeft;
    private List<Enemy> alreadyHit = new List<Enemy>();
    private void FixedUpdate()
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
                    enemy.takeDamage(damage);
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
