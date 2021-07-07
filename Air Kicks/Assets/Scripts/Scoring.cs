using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    private Text scoreText;
    public int score = 0;
    public float distanceTravelled = 0;
    private void Awake() {
        scoreText = GameObject.Find("Score").GetComponent<Text>();
    }
    
    void Update()
    {
        Vector2 pos = GameObject.FindWithTag("Player").transform.position;
        if (pos.y > distanceTravelled) distanceTravelled = pos.y;
        int temp = (int)(distanceTravelled / 5);
        if(temp > score) {
            score = temp;
        }
        scoreText.text = score.ToString();
    }
}
