using UnityEngine;
using System.Collections;

public class PlayerController2 : MonoBehaviour
{
	public float xSpeed = 10, ySpeed = 15, jumpVelocity = 20, rotSpeed = 10, wallJumpBounceSpeed = 20;
	public LayerMask playerMask;
	public bool touchingPlatform, touchingWall, touchingClimbable;
	Transform trans;
	Rigidbody2D rb;
	Vector3 reset, rotateVector;
	public Vector2 moveVel;
	public float horizInput, vertInput, jumpInput;
	public CircleCollider2D circCollider;
	public CapsuleCollider2D capsCollider;
	public float eulerAngleZ;

	void Start ()
	{
		rb = this.GetComponent<Rigidbody2D>();
		trans = this.transform;
		reset = new Vector3 (0, 2, 0);
		rb.position = reset;
		circCollider = gameObject.GetComponent<CircleCollider2D> ();
		capsCollider = gameObject.GetComponent<CapsuleCollider2D> ();
		circCollider.enabled = true;
		capsCollider.enabled = false;
	}

	void Update ()
	{
		//extra hops?? below
		//touchingClimbable = Physics2D.Linecast(trans.position, );

		//debugging
		eulerAngleZ = trans.rotation.eulerAngles.z;

		//get input keys
		horizInput = Input.GetAxisRaw ("Horizontal");
		vertInput = Input.GetAxisRaw ("Vertical");
		jumpInput = Input.GetAxisRaw ("Jump");



		//HORIZONTAL
		//if left or right keys pressed
		if (horizInput != 0) {

			//if pressing down, slide over
			if (vertInput == -1) {
				moveVel = rb.velocity;
				moveVel.x = horizInput * xSpeed * 0.3f;
			}
			//if on platform or in air, move and rotate
			else if (!touchingClimbable) {
				moveVel = rb.velocity;
				moveVel.x = horizInput * xSpeed;
				rotateVector = Vector3.back * horizInput * rotSpeed;
			}
			//if touching climbable wall, stick to it
			else {
				moveVel = rb.velocity;
				moveVel.x = horizInput * xSpeed;
				moveVel.y = 0;
				rotateVector = Vector3.zero;
			}

		} else {
			rotateVector = Vector3.zero;
			moveVel.x = 0;
		}



		//VERTICAL
		//if up or down keys pressed
		if (vertInput != 0) {

			//squish down if touching platform
			if (touchingPlatform && vertInput == -1 && horizInput == 0) {
				trans.localScale = new Vector3(5f, 2f, 1f);
				circCollider.enabled = false;
				capsCollider.enabled = true;

				//rotate to flat or upside down
				eulerAngleZ = trans.rotation.eulerAngles.z;
				if (eulerAngleZ != 0 && eulerAngleZ != 180) { 
					if (eulerAngleZ > 90 && eulerAngleZ < 270) {
						trans.rotation = Quaternion.identity;
						trans.Rotate (new Vector3 (0, 0, 180));
					} else {
						trans.rotation = Quaternion.identity;
					}
				}
				
			}
			//slide up and down climbable wall
			else if (touchingClimbable) {
				moveVel = rb.velocity;
				moveVel.y = vertInput * ySpeed;
			}
			else {
				moveVel.y = rb.velocity.y;
			}
		} else {
			moveVel.y = rb.velocity.y;
			trans.localScale = new Vector3(3.5f, 3.5f, 1f);
			circCollider.enabled = true;
			capsCollider.enabled = false;
		}



		//JUMP
		//if jump key pressed
		if (jumpInput == 1) {

			//jump if touching platform
			if (touchingPlatform) {
				moveVel = jumpVelocity * Vector2.up;
			}
			//wall jump
			else if (touchingWall) {

				moveVel = trans.InverseTransformDirection (rb.velocity);
				moveVel.y = jumpVelocity;

				moveVel.x = xSpeed * -horizInput * wallJumpBounceSpeed;
			}
			else {
				moveVel.y = rb.velocity.y;
			}

		} else {
			moveVel.y = rb.velocity.y;
		}
			
	}

	//actually move the player
	void FixedUpdate() {
		transform.Rotate (rotateVector);
		rb.velocity = moveVel;
	}

	//checks if touching various surfaces
	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "platform") {
			touchingPlatform = true;
		}
		if (other.gameObject.tag == "wall") {
			touchingWall = true;
		}
		if (other.gameObject.tag == "climbable") {
			touchingClimbable = true;
		}
		if (other.gameObject.tag == "death") {
			gameObject.transform.position = reset;
		}
	}

	//updates when no longer touching a surface
	void OnCollisionExit2D(Collision2D other) {
		if (other.gameObject.tag == "platform")
			touchingPlatform = false;
		if (other.gameObject.tag == "wall")
			touchingWall = false;
		if (other.gameObject.tag == "climbable")
			touchingClimbable = false;
	}

}