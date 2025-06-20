using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowingProjectile : HomingProjectile
{
    public float slowDuration;
    public float slowAmount;
    public override void setPower(int power)
    {
        damage *= power;
        slowDuration *= power;
    }

    public override void doDamage(Enemy e)
    {
        e.takeDamage(damage, "homing");
        StartCoroutine(slow(e));
    }
    public IEnumerator slow(Enemy e)
    {
        e.speed *= (1 - slowAmount);
        for (float i = 0; i < slowDuration; i+=Time.deltaTime)
        {
            yield return null;
        }

        e.speed /= (1 - slowAmount);
    }
}
