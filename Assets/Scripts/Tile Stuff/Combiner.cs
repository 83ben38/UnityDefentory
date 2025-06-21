using System.Collections.Generic;
using UnityEngine;

public class Combiner : MonoBehaviour
{
    public float cooldown;
    private float timeLeft;
    public int spawnAmount = 1;
    public GameObject prefab;
    private Vector2Int location;
    public Dictionary<Shape.Type, int> inventory;
    public List<Shape.Type> requirements;
    private Dictionary<Shape.Type, int> requirements2;
    public Animator animator;
    public Tile tile;
    private float baseAnimatorSpeed;
    private void Start()
    {
        inventory = new Dictionary<Shape.Type, int>();
        requirements2 = new Dictionary<Shape.Type, int>();
        baseAnimatorSpeed = animator.runtimeAnimatorController.animationClips[0].length / cooldown;
        animator.speed = baseAnimatorSpeed;
        animator.enabled = false;
        timeLeft = cooldown;
        tile = GetComponentInParent<Tile>();
        location = tile.location;
        foreach (Shape.Type type in requirements)
        {
            if (!requirements2.ContainsKey(type))
            {
                requirements2[type] = 0;
            }
            requirements2[type]++;
        }
    }

    private void FixedUpdate()
    {
        bool hasEnoughResources = true;
        foreach (Shape.Type type in requirements2.Keys)
        {
            if (!inventory.ContainsKey(type))
            {
                hasEnoughResources = false;
                break;
            }

            if (inventory[type] < requirements2[type]-tile.getPriceReduction(type))
            {
                hasEnoughResources = false;
                break;
            }
        }

        if (hasEnoughResources)
        {
            animator.enabled = true;
        }
        if (animator.enabled)
        {
            animator.speed = baseAnimatorSpeed * tile.getSpeedMultiplier();
            timeLeft -= Time.fixedDeltaTime*tile.getSpeedMultiplier();
            if (timeLeft <= 0)
            {
                for (int i = 0; i < spawnAmount; i++)
                {
                    GameObject go = Instantiate(prefab);
                    Vector3 spawnPos = transform.position;
                    if (tile.rotation % 2 == 0)
                    {
                        spawnPos.x += ((i+1f)/(spawnAmount+1)) - 0.5f;
                    }
                    else
                    {
                        spawnPos.y += ((i+1f)/(spawnAmount+1)) - 0.5f;
                    }
                    go.transform.position = spawnPos;
                    
                    go.GetComponent<Shape>().location = location;
                }
                timeLeft += cooldown;
                foreach (Shape.Type type in requirements2.Keys)
                {
                    inventory[type] -= requirements2[type];
                }
                animator.enabled = false;
            }
        }
    }
}
