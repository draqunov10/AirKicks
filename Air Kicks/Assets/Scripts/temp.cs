using UnityEngine;

public class temp : MonoBehaviour {
    // JUMP RELATED
    public Vector2 velocity;
    public float gravity = -50;
    public float drag = -13;
    public float jumpVelocity = 15;
    public float jumpSideVelocity = 6;

    // LONG JUMP RELATED
    public bool holdJump = false;
    public float holdTime = 0.3f;
    private float timer = 0;

    // GROUND RELATED
    public bool isGrounded = false;
    public float objectUpperOffset;

    // WALL RELATED
    public bool onLeftWall = false;
    public bool onRightWall = false;
    private float lWallROffset;
    private float rWallLOffset;

    private void Awake() {
        Transform leftWall = GameObject.Find("Left Wall").transform, rightWall = GameObject.Find("Right Wall").transform;
        lWallROffset = leftWall.position.x + leftWall.localScale.y / 2;
        rWallLOffset = rightWall.position.x - rightWall.localScale.y / 2;
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Ground" && col.transform.position.y + col.transform.localScale.y / 2 < transform.position.y + transform.localScale.y / 2) {
            objectUpperOffset = col.transform.position.y + col.transform.localScale.y / 2;
            isGrounded = true;
        }
        if (col.tag == "Wall") {
            velocity.x = 0;
            if (col.name == "Left Wall") onLeftWall = true;
            if (col.name == "Right Wall") onRightWall = true;
        }
        if (col.tag == "Dangerous") Debug.Log("Game Over");
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.A) && !onLeftWall) {
            velocity = new Vector2(-jumpSideVelocity, jumpVelocity);
            onRightWall = false;
            isGrounded = false;
            holdJump = true;
            timer = 0;
        }
        if (Input.GetKeyDown(KeyCode.D) && !onRightWall) {
            velocity = new Vector2(jumpSideVelocity, jumpVelocity);
            onLeftWall = false;
            isGrounded = false;
            holdJump = true;
            timer = 0;
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) holdJump = false;
    }

    private void FixedUpdate() {
        Vector2 pos = transform.position;
        if (!isGrounded) {
            if (holdJump) {
                timer += Time.deltaTime;
                if (timer >= holdTime) holdJump = false;
            }

            pos += velocity * Time.fixedDeltaTime;
            velocity.y += gravity * Time.fixedDeltaTime;
            if (!holdJump) {
                if (velocity.x < 0) velocity.x -= drag * Time.fixedDeltaTime;
                if (velocity.x > 0) velocity.x += drag * Time.fixedDeltaTime;
            }
            if (Mathf.Abs(velocity.x) <= 0.2) velocity.x = 0;
        } else {
            pos.y = objectUpperOffset + transform.localScale.y / 2;
            timer = 0;
        }

        if (onLeftWall) pos.x = lWallROffset + transform.localScale.y / 2;
        if (onRightWall) pos.x = rWallLOffset - transform.localScale.y / 2;

        transform.position = pos;
    }
}











/* ANDROID CODE IN FIXED UPDATE
 *         
for (int i = 0; i < Input.touchCount; i++)
{
    if (Input.GetTouch(i).phase == TouchPhase.Began)
    {
        //If left tap
        //body.velocity = new Vector2(-jumpSidewaysSpeed * Time.fixedDeltaTime, jumpSpeed * Time.fixedDeltaTime);
        //If right tap
        //body.velocity = new Vector2(jumpSidewaysSpeed * Time.fixedDeltaTime, jumpSpeed * Time.fixedDeltaTime);

    }
}
 */
