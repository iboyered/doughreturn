using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float xSpeed = 10, ySpeed = 15, jumpVelocity = 20;
	public LayerMask playerMask;
	public bool touchingPlatform, touchingWall, touchingClimbable, 
		canDoubleJump;
	Transform trans;
	Rigidbody2D rb;
	Vector3 reset;

	void Start ()
	{
		rb = this.GetComponent<Rigidbody2D>();
		trans = this.transform;
		reset = new Vector3 (-4, 2, 0);
		rb.position = reset;
	}

	void Update ()
	{
		//touchingClimbable = Physics2D.Linecast(trans.position, );

		Horizontal(Input.GetAxisRaw("Horizontal"));
		Vertical(Input.GetAxisRaw("Vertical"));
		if(Input.GetButtonDown("Jump"))
			Jump(Input.GetAxisRaw("Horizontal"));
	}

	void FixedUpdate() {

	}

	void Horizontal (float horizontalInput)
	{
		if (!touchingPlatform && !touchingClimbable)
			return;

		Vector2 moveVel = rb.velocity;
		moveVel.x = horizontalInput * xSpeed;
		if(touchingClimbable)
			moveVel.y = 0;
		rb.velocity = moveVel;

		if(horizontalInput != 0 && !touchingClimbable)
			transform.Rotate (Vector3.back * horizontalInput * 10);

	}

	void Vertical (float verticalInput) {
		if (touchingPlatform) {
			bool thing = true;
		} else if (touchingClimbable) {
			Vector2 moveVel = rb.velocity;
			moveVel.y = verticalInput * ySpeed;
			rb.velocity = moveVel;
		}
	}

	public void Jump(float horizontalInput)
	{
		
		if (touchingPlatform)
			rb.velocity = jumpVelocity * Vector2.up;


		else if (touchingWall) {
			//Vector2 moveVel = rb.velocity;
			Vector2 moveVel = transform.InverseTransformDirection(rb.velocity);
			moveVel.y = jumpVelocity;

			if (horizontalInput == -1) {
				moveVel.x = xSpeed * 10;
				//rb.AddForce (-transform.right * 50);
			} else if (horizontalInput == 1) {
				moveVel.x = -xSpeed * 10;
				//rb.AddForce (-transform.right * 50);
			}
			rb.velocity = moveVel;

		} else if (canDoubleJump) {
			rb.velocity = jumpVelocity * Vector2.up;
			canDoubleJump = false;
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "platform") {
			touchingPlatform = true;
			canDoubleJump = true;
		}
		if (other.gameObject.tag == "wall") {
			touchingWall = true;
			canDoubleJump = true;
		}
		if (other.gameObject.tag == "climbable") {
			touchingClimbable = true;
			canDoubleJump = true;
		}
		if (other.gameObject.tag == "death") {
			gameObject.transform.position = reset;
			canDoubleJump = true;
		}
	}

	void OnCollisionExit2D(Collision2D other) {
		if (other.gameObject.tag == "platform")
			touchingPlatform = false;
		if (other.gameObject.tag == "wall")
			touchingWall = false;
		if (other.gameObject.tag == "climbable")
			touchingClimbable = false;
	}

}