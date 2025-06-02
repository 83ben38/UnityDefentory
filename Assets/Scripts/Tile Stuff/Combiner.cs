using System.Collections.Generic;
using UnityEngine;

public class Combiner : MonoBehaviour
{
    public float cooldown;
    private float timeLeft;
    public GameObject prefab;
    private Vector2Int location;
    public Dictionary<Shape.Type, int> inventory;
    public List<Shape.Type> requirements;
    private Dictionary<Shape.Type, int> requirements2;
    public Animator animator;
    private void Start()
    {
        inventory = new Dictionary<Shape.Type, int>();
        requirements2 = new Dictionary<Shape.Type, int>();
        animator.speed = animator.runtimeAnimatorController.animationClips[0].length / cooldown;
        animator.enabled = false;
        timeLeft = cooldown;
        location = GetComponentInParent<Tile>().location;
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

            if (inventory[type] < requirements2[type])
            {
                hasEnoughResources = false;
            }
        }

        if (hasEnoughResources)
        {
            animator.enabled = true;
        }
        if (animator.enabled)
        {
            timeLeft -= Time.fixedDeltaTime;
            if (timeLeft <= 0)
            {
                GameObject go = Instantiate(prefab);
                go.transform.position = transform.position;
                go.GetComponent<Shape>().location = location;
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
