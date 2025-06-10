using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public Enemy target;
    public abstract void setPower(int power);
}
