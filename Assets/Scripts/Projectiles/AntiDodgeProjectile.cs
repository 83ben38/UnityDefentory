

public class AntiDodgeProjectile : HomingProjectile
{
    public override void doDamage(Enemy e)
    {
        e.takeDamage(damage, "anti-dodge");
    }
}
