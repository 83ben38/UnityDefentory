
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * 0.1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * 0.1f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * 0.1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * 0.1f;
        }
    }
}
