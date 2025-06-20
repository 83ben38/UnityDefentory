using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakeningProjectile : HomingProjectile
{
    public float weakenAmount;
    public float weakenDuration;
    public override void setPower(int power)
    {
        damage *= power;
        weakenAmount *= power;
    }

    public override void doDamage(Enemy e)
    {
        e.takeDamage(damage, "homing");
        StartCoroutine(weaken(e));
    }
    public IEnumerator weaken(Enemy e)
    {
        e.bonusDamage += weakenAmount;
        for (float i = 0; i < weakenDuration; i+=Time.deltaTime)
        {
            yield return null;
        }

        e.bonusDamage -= weakenAmount;
    }
}
