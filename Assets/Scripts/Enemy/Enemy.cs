
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public Type shapeType;
    public enum Type{
        Circle,
        Square,
        Triangle,
        Plus,
        Hexagon,
        Star,
        Diamond
    }

    public int size;
    private float hp;
    public bool canHeal = true;
    public float maxHP;
    public float speed;
    public GameObject hpBar;
    public GameObject damageBar;
    public Vector2Int location;
    public static List<Enemy> enemies = new List<Enemy>();
    private bool hpBarShowing = false;
    private int dodgeCounter = 0;
    public float stunDuration = 0f;
    public float bonusDamage = 0f;
    private void Start()
    {
        for (int i = 0; i < size-1; i++)
        {
            switch (shapeType)
            {
                case Type.Circle: case Type.Plus: maxHP *= 5; break;
                case Type.Square: maxHP *= 7;
                    speed *= 0.8f;
                    break;
                case Type.Triangle: maxHP *= 3;
                    speed *= 1.3f; break;
                case Type.Hexagon: maxHP *= 4; break;
                case Type.Star: case Type.Diamond: maxHP *= 3; break;
            }
            
        }
        hp = maxHP;
        hpBar.SetActive(false);
        damageBar.SetActive(false);
        transform.position = Vector3.zero;
        location = new Vector2Int(0, 0);
        CheckLocation(true);
        enemies.Add(this);
    }

    private void OnDestroy()
    {
        enemies.Remove(this);
    }

    public void takeDamage(float damageAmount, string damageSource)
    {

        damageAmount += bonusDamage;
        if (shapeType == Type.Hexagon)
        {
            damageAmount -= size;
            if (damageAmount <= 0)
            {
                return;
            }
        }
        if (shapeType == Type.Diamond)
        {
            if (damageSource == "anti-dodge")
            {
                dodgeCounter = size;
            }
            else
            {
                if (dodgeCounter == size)
                {
                    dodgeCounter = 0;
                }
                else
                {
                    dodgeCounter++;
                    return;
                }
            }
        }
        if (!hpBarShowing)
        {
            hpBar.SetActive(true);
            damageBar.SetActive(true);
            hpBarShowing = true;
        }
        hp -= damageAmount;
        if (hp < 1)
        {
            Destroy(gameObject);
        }
        Vector3 scale = damageBar.transform.localScale;
        Vector3 pos = damageBar.transform.localPosition;
        hpBar.transform.localScale = new Vector3(scale.x, scale.y * hp / maxHP);
        hpBar.transform.localPosition = new Vector3(pos.x  - (scale.y * (1 - (hp / maxHP))) / 2, pos.y, 0);
    }
    public void heal(float healAmount)
    {
        if (!canHeal)
        {
            return;
        }
        hp += healAmount;
        if (hp >= maxHP)
        {
            hp = maxHP;
            hpBar.SetActive(false);
            damageBar.SetActive(false);
            hpBarShowing = false;
        }
        else
        {
            Vector3 scale = damageBar.transform.localScale;
            Vector3 pos = damageBar.transform.localPosition;
            hpBar.transform.localScale = new Vector3(scale.x, scale.y * hp / maxHP);
            hpBar.transform.localPosition = new Vector3(pos.x - (scale.y * (1 - (hp / maxHP))) / 2, pos.y, 0);
        }
    }
    private void CheckLocation(bool start)
    {
        if (start)
        {
            StartCoroutine(StartTile());
            return;
        }
        if (EnemyPathingManager.instance.isEnd(location))
        {
            StartCoroutine(Vortex());
            return;
        }
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
            case Tile.Type.EnemyBelt: StartCoroutine(Belt()); break;
            default: Destroy(gameObject); break;
        }
    }
    public IEnumerator StartTile()
    {
        Tile on = GridController.instance.grid[location];
        Vector2Int desinationTile;
        Vector3 start = transform.position;
        Vector3 end;
        desinationTile = EnemyPathingManager.instance.getNextTile();
        end = new Vector3(desinationTile.x,desinationTile.y);
        Vector3 difference = end - start;
        for (float i = 0; i < difference.magnitude; i+=Time.deltaTime*speed*on.getSpeedMultiplier())
        {
            transform.position = start + difference * i / difference.magnitude;
            yield return null;
        }

        transform.position = end;
        location = desinationTile;
        CheckLocation(false);
    }
    public IEnumerator Vortex()
    {
        Vector3 start = transform.position;
        Vector3 end = new Vector3(location.x,location.y);
        Vector3 difference = end - start;
        float startingScale = transform.localScale.x;
        for (float i = 0; i < 1; i+=Time.deltaTime*speed)
        {
            transform.position = start + difference * i;
            transform.localScale = new Vector3(startingScale * (1-i),startingScale * (1-i));
            yield return null;
        }

        LivesManager.instance.TakeDamage((int)hp);
        Destroy(gameObject);
    }
    public IEnumerator Belt()
    {
        if (shapeType == Type.Plus)
        {
            SpriteRenderer sp = GetComponent<SpriteRenderer>();
            transform.localScale *= 2f;
            sp.color = new Color(0, 125, 0, 125);
            foreach (Enemy enemy in enemies)
            {
            
                if (enemy != this) if ((enemy.transform.position - transform.position).magnitude <= transform.localScale.x + enemy.transform.localScale.x)
                {
                    enemy.heal(size * size);
                }

            }
        

            for (float i = 0; i < 0.2f; i += Time.deltaTime)
            {
                yield return null;
            }

            sp.color = Color.red;
            transform.localScale *= 0.5f;
        }

        if (shapeType == Type.Star)
        {
            if (Random.Range(0, 4) == 0)
            {
                int currentD = findDistance(location);
                List<Vector2Int> possibleDestinations = new List<Vector2Int>();
                for (int i = -size; i <= size; i++)
                {
                    for (int j = -size; j <= size; j++)
                    {
                        int d = findDistance(new Vector2Int(location.x+i,location.y+j));
                        if (d != -1 && d < currentD)
                        {
                            possibleDestinations.Add(new Vector2Int(location.x+i,location.y+j));
                        }
                    }
                }

                if (possibleDestinations.Count != 0)
                {
                    Vector2Int randomDestination = possibleDestinations[Random.Range(0, possibleDestinations.Count)];
                    var transform1 = transform;
                    float startingScale = transform1.localScale.x;
                    for (float i = 1; i >= 0; i -= Time.deltaTime*3)
                    {
                        transform1.localScale = new Vector3(i*startingScale, i*startingScale);
                        transform1.eulerAngles = new Vector3(0, 0, i * 360);
                        yield return null;
                    }
                    transform1.position = new Vector3(randomDestination.x+ Random.value * .7f - .35f, randomDestination.y+ Random.value * .7f - .35f);
                    location = randomDestination;
                    for (float i = 0; i <= 1; i += Time.deltaTime*3)
                    {
                        transform1.localScale = new Vector3(i*startingScale, i*startingScale);
                        transform1.eulerAngles = new Vector3(0, 0, i * 360);
                        yield return null;
                    }
                    transform1.localScale = new Vector3(startingScale,startingScale);
                    transform1.eulerAngles = new Vector3(0, 0, 0);
                    CheckLocation(false);
                    yield break;
                }
            }
        }
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
        for (float i = 0; i < difference.magnitude; i+=Time.deltaTime*speed*on.getSpeedMultiplier())
        {
            if (stunDuration > 0f)
            {
                stunDuration -= Time.deltaTime * speed;
                i -= Time.deltaTime * speed;
                if (stunDuration <= 0f)
                {
                    i -= stunDuration;
                    stunDuration = 0f;
                }
            }
            transform.position = start + difference * i / difference.magnitude;
            yield return null;
        }

        transform.position = end;
        location = desinationTile;
        CheckLocation(false);
    }
    public int findDistance(Vector2Int start)
    {
        if (EnemyPathingManager.instance.isEnd(start))
        {
            return 0;
        }
        if (!GridController.instance.grid.ContainsKey(start))
        {
            return -1;
        }
        Tile t = GridController.instance.grid[start];
        if (t.tileType != Tile.Type.EnemyBelt)
        {
            return -1;
        }
        if (t.rotation % 2 == 0)
        {
            return findDistance(start + new Vector2Int(0,1-t.rotation))+1;
        }

        return findDistance(start + new Vector2Int(t.rotation - 2, 0))+1;
    }
}
