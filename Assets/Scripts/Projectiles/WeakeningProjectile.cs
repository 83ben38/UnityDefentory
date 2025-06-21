using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakeningProjectile : HomingProjectile
{
    public float weakenAmount;
    public float weakenDuration;
    public override void setPower(float power, Tile t)
    {
        damage += t.getDamageAddition();
        pierce += t.getPierceAddition();
        pierce += t.getPierceAddition();
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
