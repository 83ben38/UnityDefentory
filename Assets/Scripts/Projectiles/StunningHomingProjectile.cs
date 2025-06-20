using System.Collections.Generic;
using UnityEngine;

public class StunningHomingProjectile : HomingProjectile
{
    public float stunDuration;
    public override void setPower(int power)
    {
        damage *= power;
        stunDuration *= power;
    }

    public override void doDamage(Enemy e)
    {
        e.takeDamage(damage, "homing");
        e.stunDuration += stunDuration;
    }
}
