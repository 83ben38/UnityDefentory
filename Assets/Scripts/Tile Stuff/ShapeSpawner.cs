using UnityEngine;

public class ShapeSpawn : MonoBehaviour
{
    public float cooldown;
    private float timeLeft;
    public GameObject prefab;
    private Vector2Int location;
    public Tile tile;
    private void Start()
    {
        Animator animator = GetComponent<Animator>();
        animator.speed = animator.runtimeAnimatorController.animationClips[0].length / cooldown;
        timeLeft = cooldown;
        tile = GetComponentInParent<Tile>();
        location = tile.location;
    }

    private void FixedUpdate()
    {
        timeLeft -= Time.fixedDeltaTime;
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
