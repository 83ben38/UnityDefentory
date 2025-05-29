using UnityEngine;

public class ShapeSpawner : MonoBehaviour
{
    public float cooldown;
    private float timeLeft;
    public GameObject prefab;
    private Vector2Int location;
    private void Start()
    {
        Animator animator = GetComponent<Animator>();
        animator.speed = 1.15f / cooldown;
        timeLeft = cooldown;
        location = GetComponentInParent<Tile>().location;
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
