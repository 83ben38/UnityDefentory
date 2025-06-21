using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public Enemy target;
    public abstract void setPower(float power, Tile t);
    public abstract void doDamage(Enemy e);
}
