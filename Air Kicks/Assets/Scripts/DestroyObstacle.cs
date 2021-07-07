using UnityEngine;

public class DestroyObstacle : MonoBehaviour
{
    private Vector2 screenBounds;

    void Update()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        if (transform.position.y < screenBounds.y - 12) Destroy(this.gameObject);
    }
}
