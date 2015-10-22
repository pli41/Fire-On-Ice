using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	//start from 1
	public int joystickNum;
	public float speed;
	public float accFactor;
	public float minSpeed;

	public bool canMove;
	public bool canTurn;

	public bool disabled;

	Vector3 dodgeDir;

	private Vector3 dodgePos;
	private float dodgeInTimer;
	private float dodgeTimer;
	private Vector3 movement;
	private Animator anim;
	private Rigidbody playerRigidbody;
	private bool dodgeInit;
	public float maxSpeed;
	void Awake(){
		anim = GetComponent<Animator> ();
		playerRigidbody = GetComponent<Rigidbody> ();
		dodgeInit = true;
		canMove = true;
		canTurn = true;
	}

	void FixedUpdate(){
		dodgeTimer += Time.deltaTime;
		
		float h = ControllerInputWrapper.GetAxisRaw (ControllerInputWrapper.Axis.LeftStickX, joystickNum, false);
		float v = ControllerInputWrapper.GetAxisRaw (ControllerInputWrapper.Axis.LeftStickY, joystickNum, false);

		//Debug.Log ("h = " + h + " v = " + v);
		if(!disabled){
			if(canMove || canTurn){
				if(canMove){
					Move (h, v);

				}
				if(canTurn){
					Turning ();
				}

			}
		}
		Animating (h, v);
//		if((dodgeTimer >= timeBetDodge && Input.GetAxisRaw("PS4_L2_" + playerString) > 0 && dodgeInit == true) || dodgeInit == false){
//			Debug.Log("dodge start");
//			Dodge(h, v);
//		}
		
		
		
	}
	
	void Move(float h, float v){
		//movement.Set (h, 0f, v);
		//movement = movement.normalized * speed * Time.deltaTime;
		//playerRigidbody.MovePosition (transform.position + movement);

		
		movement.Set (h, 0f, v);
		float acc = Mathf.Sqrt (h * h + v * v);

		speed = acc * maxSpeed / 0.09f;

		if(speed < minSpeed){
			speed = minSpeed;
		}
		if(speed > maxSpeed){
			speed = maxSpeed;
		}

		movement = movement.normalized * speed * acc * accFactor;
		//playerRigidbody.MovePosition (transform.position + movement);
		//Debug.Log ("Magnitude = " + playerRigidbody.velocity.magnitude);
		
		if(h != 0 || v != 0){
			//Debug.Log("Input received");
			if(playerRigidbody.velocity.magnitude < maxSpeed){
				//Debug.Log("Accelerating");
				playerRigidbody.velocity += movement;
				//Debug.Log(playerRigidbody.velocity);
			}
			else{
				playerRigidbody.velocity = playerRigidbody.velocity.normalized * maxSpeed;

			}
			
		}
	}
	
	void Turning(){
		float hori = ControllerInputWrapper.GetAxisRaw(ControllerInputWrapper.Axis.RightStickX, joystickNum);
		float vert = ControllerInputWrapper.GetAxisRaw (ControllerInputWrapper.Axis.RightStickY, joystickNum);
		
		if(hori != 0 && vert != 0){
			Vector3 direction = new Vector3 (hori, 0f, vert);
			
			Quaternion newRotation = Quaternion.LookRotation(direction);
			playerRigidbody.MoveRotation(newRotation);
			//Debug.Log ("hori = " + hori);
			//Debug.Log ("vert = " + vert);
		}
		
		
		/*Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit floorHit;

		if(Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)){
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;

			Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
			playerRigidbody.MoveRotation(newRotation);

		}*/
	}
	
	void Animating(float h, float v){
		bool walking = (h != 0f || v != 0f) && canMove;
		anim.SetBool ("IsWalking", walking);
	}
	
	
	
	
}
