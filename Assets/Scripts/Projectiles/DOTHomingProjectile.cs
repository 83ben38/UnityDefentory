using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOTHomingProjectile : HomingProjectile
{
    public float DOTDamage;
    public float interval;
    public int numHits;
    public override void setPower(float power, Tile t)
    {
        damage += t.getDamageAddition();
        pierce += t.getPierceAddition();
        damage *= power;
        DOTDamage *= power;
        numHits = (int) power * numHits;
    }
    

    public override void doDamage(Enemy e)
    {
        e.takeDamage(damage, "homing");
        StartCoroutine(doDOTDamage(e));
    }

    public IEnumerator doDOTDamage(Enemy e)
    {
        for (int i = 0; i < numHits; i++)
        {
            for (float j = 0; j < interval; j+=Time.deltaTime)
            {
                if (!e.gameObject)
                {
                    yield break;
                }
                e.takeDamage(DOTDamage,"DOT");
                yield return null;
            }
        }
    }
}
