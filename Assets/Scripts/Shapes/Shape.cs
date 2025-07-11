using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shape : MonoBehaviour
{
    public Type shapeType;
    public enum Type{
        Circle,
        //tier 1
        Square,
        Triangle,
        Pentagon,
        Lightning,
        Arrow,
        //tier 2
        Rectangle,
        Trapezoid,
        Hexagon,
        Star,
        Diamond
    }
    public static float getPower(Type shape)
    {
        float baseMultiplier = 1f;
        if (UpgradeManager.instance.isUpgradeAvailable(UpgradeCard.Upgrade.IncreasePowerStorage))
        {
            if (ResourceManager.instance.resources.ContainsKey(shape))
            {
                baseMultiplier *= Mathf.Max(1f, Mathf.Log10(ResourceManager.instance.resources[shape]));
            }
        }
        switch (shape)
        {
            case Type.Circle: return baseMultiplier * (UpgradeManager.instance.isUpgradeAvailable(UpgradeCard.Upgrade.CirclePowerUp) ? 2 : 1);
            case Type.Square: return  baseMultiplier * (UpgradeManager.instance.isUpgradeAvailable(UpgradeCard.Upgrade.SquarePowerUp) ? 4 : 3);
            case Type.Triangle: return  baseMultiplier * (UpgradeManager.instance.isUpgradeAvailable(UpgradeCard.Upgrade.TrianglePowerUp) ? 4 : 3);
            case Type.Pentagon: return  baseMultiplier * (UpgradeManager.instance.isUpgradeAvailable(UpgradeCard.Upgrade.PentagonPowerUp) ? 4 : 3);
            case Type.Lightning: return  baseMultiplier * (UpgradeManager.instance.isUpgradeAvailable(UpgradeCard.Upgrade.LightningPowerUp) ? 4 : 3);
            case Type.Arrow: return  baseMultiplier * (UpgradeManager.instance.isUpgradeAvailable(UpgradeCard.Upgrade.ArrowPowerUp) ? 4 : 3);
            case Type.Rectangle: return  baseMultiplier * (UpgradeManager.instance.isUpgradeAvailable(UpgradeCard.Upgrade.SquarePowerUp) ? 13 : 10); 
            case Type.Trapezoid: return  baseMultiplier * (UpgradeManager.instance.isUpgradeAvailable(UpgradeCard.Upgrade.TrianglePowerUp) ? 13 : 10); 
            case Type.Hexagon: return  baseMultiplier * (UpgradeManager.instance.isUpgradeAvailable(UpgradeCard.Upgrade.PentagonPowerUp) ? 13 : 10); 
            case Type.Star: return  baseMultiplier * (UpgradeManager.instance.isUpgradeAvailable(UpgradeCard.Upgrade.LightningPowerUp) ? 13 : 10); 
            case Type.Diamond: return  baseMultiplier * (UpgradeManager.instance.isUpgradeAvailable(UpgradeCard.Upgrade.ArrowPowerUp) ? 13 : 10); 
            default: throw new Exception();
        }
    }
    public Vector2Int location;
    public SpriteRenderer spriteRenderer;
    private void Start()
    {
        CheckLocation(true);
    }

    private void CheckLocation(bool start)
    {
        if (!GridController.instance.grid.ContainsKey(location))
        {
            Destroy(gameObject);
            return;
        }
        Tile on = GridController.instance.grid[location];
        if (!on)
        {
            Destroy(gameObject);
        }
        switch (on.tileType)
        {
            case Tile.Type.Belt: StartCoroutine(Belt()); break;
            case Tile.Type.Splitter: StartCoroutine(Belt());
                on.rotation += 2;
                on.rotation %= 4; break;
            case Tile.Type.Combiner:
                if (start)
                {
                    StartCoroutine(Belt());
                }
                else
                {
                    Combiner combiner = on.GetComponent<Combiner>();
                    if (!combiner.inventory.ContainsKey(shapeType))
                    {
                        combiner.inventory[shapeType] = 0;
                    }
                    combiner.inventory[shapeType]++;
                    Destroy(gameObject);
                }
                break;
            case Tile.Type.Turret: 
                Turret turret = on.GetComponent<Turret>();
                if (!turret.inventory.ContainsKey(shapeType))
                {
                    turret.inventory[shapeType] = 0;
                }
                turret.inventory[shapeType]++;
                Destroy(gameObject);
                break;
            case Tile.Type.Vortex: StartCoroutine(Vortex()); break;
            case Tile.Type.Spawner:
                if (start)
                {
                    StartCoroutine(Belt());
                }
                else
                {
                    Destroy(gameObject);
                }
                break;
            case Tile.Type.Tunnel: StartCoroutine(Tunnel()); break;
            default: Destroy(gameObject); break;
        }
    }

    public IEnumerator Vortex()
    {
        Vector3 start = transform.position;
        Vector3 end = new Vector3(location.x,location.y);
        Vector3 difference = end - start;
        float startingScale = transform.localScale.x;
        for (float i = 0; i < 1; i+=Time.deltaTime)
        {
            transform.position = start + difference * i;
            transform.localScale = new Vector3(startingScale * (1-i),startingScale * (1-i));
            yield return null;
        }

        if (!ResourceManager.instance.resources.ContainsKey(shapeType))
        {
            ResourceManager.instance.resources[shapeType] = 0;
        }
        ResourceManager.instance.resources[shapeType] += 1;
        Destroy(gameObject);
    }
    public IEnumerator Belt()
    {
        Tile on = GridController.instance.grid[location];
        Vector2Int desinationTile;
        Vector3 start = transform.position;
        Vector3 end;
        if (on.rotation % 2 == 0)
        {
            desinationTile = on.location + new Vector2Int(0,1-on.rotation);
            end = new Vector3(start.x,desinationTile.y + Random.value * .7f - .35f);
        }
        else
        {
            desinationTile = on.location + new Vector2Int(on.rotation-2,0);
            end = new Vector3(desinationTile.x + Random.value * .7f - .35f,start.y);
        }

        Vector3 difference = end - start;
        for (float i = 0; i < difference.magnitude; i+=Time.deltaTime*on.getSpeedMultiplier())
        {
            transform.position = start + difference * i / difference.magnitude;
            yield return null;
        }

        transform.position = end;
        location = desinationTile;
        CheckLocation(false);
    }
    public IEnumerator Tunnel()
    {
        Tile on = GridController.instance.grid[location];
        Vector2Int desinationTile = on.GetComponent<Tunnel>().endingLocation;
        Vector3 start = transform.position;
        Vector3 end;
        end = new Vector3(desinationTile.x,desinationTile.y);

        Vector3 difference = end - start;
        for (float i = 0; i < difference.magnitude; i+=Time.deltaTime*on.getSpeedMultiplier())
        {
            transform.position = start + difference * i / difference.magnitude;
            if ((transform.position - new Vector3(on.location.x,on.location.y)).sqrMagnitude > 0.1f && (transform.position - new Vector3(desinationTile.x,desinationTile.y)).sqrMagnitude > 0.1f)
            {
                spriteRenderer.enabled = false;
            }
            else
            {
                spriteRenderer.enabled = true;
            }
            yield return null;
        }

        transform.position = end;
        location = desinationTile;
        CheckLocation(false);
    }
}
