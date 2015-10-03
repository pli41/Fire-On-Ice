using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public string playerString;
	public float speed;
	public float accFactor;

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
	
	void Awake(){
		anim = GetComponent<Animator> ();
		playerRigidbody = GetComponent<Rigidbody> ();
		dodgeInit = true;
		canMove = true;
		canTurn = true;
	}

	void FixedUpdate(){
		dodgeTimer += Time.deltaTime;
		
		float h = Input.GetAxisRaw ("PS4_Horizontal_" + playerString);
		float v = Input.GetAxisRaw ("PS4_Vertical_" + playerString);

		if(!disabled){
			if(canMove || canTurn){
				if(canMove){
					Move (h, v);
				}
				if(canTurn){
					Turning ();
				}
				Animating (h, v);
			}
		}

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
		movement = movement.normalized * speed * accFactor;
		//playerRigidbody.MovePosition (transform.position + movement);
		//Debug.Log ("Magnitude = " + playerRigidbody.velocity.magnitude);
		
		if(h != 0 || v != 0){
			//Debug.Log("Input received");
			if(playerRigidbody.velocity.magnitude < speed){
				//Debug.Log("Accelerating");
				playerRigidbody.velocity += movement;
				//Debug.Log(playerRigidbody.velocity);
			}
			else{
				//Debug.Log("MAX speed");
			}
			
		}
	}
	
	void Turning(){
		float hori = Input.GetAxis("PS4_RightAnalogHorizontal_" + playerString);
		float vert = Input.GetAxis("PS4_RightAnalogVertical_" + playerString);
		
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
		bool walking = h != 0f || v != 0f;
		anim.SetBool ("IsWalking", walking);
	}
	
	
	
	
}
