using UnityEngine;

public class SpawnObstacle : MonoBehaviour
{
    public GameObject[] obstacles;
    //public int haveSpawned = 0, spawnHeight = 10, haventSpawned;
    private Vector2 screenBounds;
    public int heightY, startY;

    private void Start() {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        startY = (int) transform.position.y;
    }

    void Update() {
        heightY = (int) transform.position.y + 10;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        if (heightY%2 == 0 && startY + 2 <= heightY) {
            GameObject temp = Instantiate(obstacles[(int)Random.Range(0, obstacles.Length - 1)]) as GameObject;
            temp.transform.parent = GameObject.Find("Obstacles").transform;
            temp.transform.position = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), startY);
            startY += 2;
        }

    }
}
