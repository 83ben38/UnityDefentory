using UnityEngine;

public class ShapeSpawn : MonoBehaviour
{
    public float cooldown;
    private float timeLeft;
    public GameObject prefab;
    private Vector2Int location;
    public Tile tile;
    private float baseAnimatorSpeed;
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        baseAnimatorSpeed = animator.runtimeAnimatorController.animationClips[0].length / cooldown;
        animator.speed = baseAnimatorSpeed;
        timeLeft = cooldown;
        tile = GetComponentInParent<Tile>();
        location = tile.location;
    }

    private void FixedUpdate()
    {
        animator.speed = baseAnimatorSpeed * tile.getSpeedMultiplier();   
        timeLeft -= Time.fixedDeltaTime*tile.getSpeedMultiplier();
        if (timeLeft <= 0)
        {
            //create clone of circle
            GameObject go = Instantiate(prefab);
            go.transform.position = transform.position;
            go.GetComponent<Shape>().location = location;
            timeLeft += cooldown;
        }
    }
}
