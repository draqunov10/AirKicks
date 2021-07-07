using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    public float speed = 5f;
    public float slideSpeed = 0.5f;
    public bool start = false;
    void Update() {
        player = GameObject.FindWithTag("Player").transform;
        Vector3 temp = transform.position;

        if (player.position.y >= transform.position.y)
        {
            temp.y = player.position.y;
            transform.position = Vector3.MoveTowards(transform.position, temp, speed * Time.deltaTime);
            start = true;
        }

        if (start) {
            temp.y += transform.position.y + 5;
            transform.position = Vector3.MoveTowards(transform.position, temp, slideSpeed * Time.deltaTime);
        }
    }
}
