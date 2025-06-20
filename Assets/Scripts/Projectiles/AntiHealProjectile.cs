using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiHealProjectile : HomingProjectile
{
    public float antiHealDuration;
    public override void setPower(int power)
    {
        damage *= power;
        antiHealDuration *= power;
    }


    public override void doDamage(Enemy e)
    {
        e.takeDamage(damage, "homing");
        StartCoroutine(antiHeal(e));
    }

    public IEnumerator antiHeal(Enemy e)
    {
        e.canHeal = false;
        for (float i = 0; i < antiHealDuration; i+=Time.deltaTime)
        {
            yield return null;
        }

        e.canHeal = true;
    }
}
