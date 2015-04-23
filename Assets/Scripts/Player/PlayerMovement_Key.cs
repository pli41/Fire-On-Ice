using UnityEngine;

public class PlayerMovement_Key : MonoBehaviour
{
	public float speed = 6f;
	public float timeBetDodge = 1f;
	public float dodgeDist = 3f;
	public float dodgeSpeed = 15f;
	public float accFactor = 0.1f;

	private Vector3 dodgePos;
	private float dodgeTimer;
	private Vector3 movement;
	private Animator anim;
	private Rigidbody playerRigidbody;
	private int floorMask;
	private float camRayLength = 100f;
	private bool dodgeInit;
	
	void Awake(){
		floorMask = LayerMask.GetMask("Floor");
		anim = GetComponent<Animator> ();
		playerRigidbody = GetComponent<Rigidbody> ();
		dodgeInit = false;
	}
	
	void FixedUpdate(){
		dodgeTimer += Time.deltaTime;
		
		float h = Input.GetAxisRaw ("Horizontal_Keyboard");
		float v = Input.GetAxisRaw ("Vertical_Keyboard");
		
		if(anim.GetBool("IsDodging")){
			Dodge();
		}
		else{
			Move (h, v);
			Turning ();
			Animating (h, v);
			
			if(dodgeTimer >= timeBetDodge && Input.GetButtonDown("Jump")){
				Debug.Log("dodge start");
				Dodge();
				dodgeInit = true;
			}
		}
		
		
		
	}
	
	void Move(float h, float v){
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
		
		
		/*float hori = Input.GetAxis("PS4_RightAnalogHorizontal");
		float vert = Input.GetAxis("PS4_RightAnalogVertical");
		
		if(hori != 0 && vert != 0){
			Vector3 direction = new Vector3 (hori, 0f, vert);
			
			Quaternion newRotation = Quaternion.LookRotation(direction);
			playerRigidbody.MoveRotation(newRotation);
			Debug.Log ("hori = " + hori);
			Debug.Log ("vert = " + vert);
		}*/
		
		
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit floorHit;

		if(Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)){
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;

			Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
			playerRigidbody.MoveRotation(newRotation);

		}
	}
	
	void Dodge(){
		dodgeTimer = 0f;
		anim.SetBool ("IsDodging", true);
		
		if(dodgeInit){
			Debug.Log("Initalize dodge");
			Vector3 dodgeMov = transform.forward * dodgeDist;
			dodgePos = transform.position + dodgeMov;
			dodgeInit = false;
		}
		
		if(Vector3.Distance(transform.position, dodgePos) < 0.5f){
			Debug.Log("dodge complete");
			anim.SetBool("IsDodging", false);
			dodgePos = transform.position;
		}
		else{
			Vector3 dodge = transform.forward * dodgeSpeed * Time.deltaTime;
			playerRigidbody.MovePosition (transform.position + dodge);
		}
		
	}
	
	void Animating(float h, float v){
		bool walking = h != 0f || v != 0f;
		anim.SetBool ("IsWalking", walking);
	}
	
	
	
	
}
