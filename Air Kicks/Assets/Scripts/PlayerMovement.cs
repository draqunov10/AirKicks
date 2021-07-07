using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    // JUMP RELATED
    public Vector2 velocity;
    public float gravity = -40;
    public float drag = -13;
    public float jumpVelocity = 12;
    public float jumpSideVelocity = 6;

    // LONG JUMP RELATED
    public bool holdJump = false;
    public float holdTime = 0.3f;
    private float timer = 0;

    // GROUND RELATED
    public bool isGrounded = false;
    public bool onCeiling = false;

    // WALL RELATED
    public bool onLeftWall = false;
    public bool onRightWall = false;

    public GameObject gameOver;
    public CameraFollow cameraFollow;

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.collider.tag == "Wall") {
            if (col.collider.transform.position.x < transform.position.x) onLeftWall = true;
            else onRightWall = true;
            velocity.x = 0;
        }
        if (col.collider.tag == "Ground") {
            if (transform.position.y > col.collider.transform.position.y) isGrounded = true;
            else onCeiling = true;
            velocity.y = 0;
        }

        if (col.collider.tag == "Dangerous") {
            gameOver.SetActive(true);
            GameObject.FindWithTag("Pause Button").SetActive(false);
            this.enabled = false;
            Invoke("disableCamera", 1f);
        }
    }

    void disableCamera() { cameraFollow.enabled = false; }
    private void OnCollisionExit2D(Collision2D col) {
        if (col.collider.tag == "Wall") {
            if (onLeftWall) onLeftWall = false;
            else onRightWall = false;
        }
        if (col.collider.tag == "Ground") {
            if (onCeiling) onCeiling = false;
        }
    }

    private void Update() {
        GetKeys();
        Movement();
    }

    private void GetKeys() {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)) {
            isGrounded = false;
            holdJump = true;
            timer = 0;
        }
        if (Input.GetKeyDown(KeyCode.A)) {
            if (!onLeftWall && !onCeiling) velocity = new Vector2(-jumpSideVelocity, jumpVelocity);
            if (onLeftWall && !onCeiling) velocity = new Vector2(0, jumpVelocity);
            if (!onLeftWall && onCeiling) velocity = new Vector2(-jumpSideVelocity, 0);
            onRightWall = false;
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            if (!onRightWall && !onCeiling) velocity = new Vector2(jumpSideVelocity, jumpVelocity);
            if (onRightWall && !onCeiling) velocity = new Vector2(0, jumpVelocity);
            if (!onRightWall && onCeiling) velocity = new Vector2(jumpSideVelocity, 0);
            onLeftWall = false;
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) holdJump = false;
    }

    private void Movement() {
        Vector2 pos = transform.position;

        if (!isGrounded) {
            if (holdJump) {
                timer += Time.deltaTime;
                if (timer >= holdTime) holdJump = false;
            }

            pos += velocity * Time.deltaTime;
            velocity.y += gravity * Time.deltaTime;
            if (!holdJump) {
                if (velocity.x < 0) velocity.x -= drag * Time.deltaTime;
                if (velocity.x > 0) velocity.x += drag * Time.deltaTime;
            }
            if (Mathf.Abs(velocity.x) <= 0.2) velocity.x = 0;
        }

        transform.position = pos;
    }


}
