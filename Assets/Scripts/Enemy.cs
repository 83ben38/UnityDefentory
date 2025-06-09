
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public Type shapeType;
    public enum Type{
        Circle,
        Square
    }

    public int size;
    private float hp;
    public float maxHP;
    public GameObject hpBar;
    public GameObject damageBar;
    public Vector2Int location;
    private void Start()
    {
        for (int i = 0; i < size-1; i++)
        {
            maxHP *= 5;
        }
        hp = maxHP;
        hpBar.SetActive(false);
        damageBar.SetActive(false);
        transform.position = Vector3.zero;
        location = new Vector2Int(0, 0);
        CheckLocation(true);
    }

    public void takeDamage(float damageAmount)
    {
        if (hp == maxHP)
        {
            hpBar.SetActive(true);
            damageBar.SetActive(true);
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
        for (float i = 0; i < difference.magnitude; i+=Time.deltaTime)
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
        for (float i = 0; i < 1; i+=Time.deltaTime)
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
        for (float i = 0; i < difference.magnitude; i+=Time.deltaTime)
        {
            transform.position = start + difference * i / difference.magnitude;
            yield return null;
        }

        transform.position = end;
        location = desinationTile;
        CheckLocation(false);
    }
}
