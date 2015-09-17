using UnityEngine;

public class PlayerMovement_Key : MonoBehaviour
{
	public float speed;
	public float timeBetDodge = 1f;
	public float dodgeDist = 3f;
	public float dodgeSpeed = 15f;
	public float accFactor = 0.1f;
	
	Vector3 dodgeDir;
	private float dodgeInTimer;
	private float dodgeTimer;
	private Vector3 movement;
	private Animator anim;
	private Rigidbody playerRigidbody;
	private bool dodgeInit;
	
	void Awake(){
		anim = GetComponent<Animator> ();
		playerRigidbody = GetComponent<Rigidbody> ();
		dodgeInit = false;
	}
	
	void FixedUpdate(){
		dodgeTimer += Time.deltaTime;
		
		float h = Input.GetAxisRaw ("Horizontal_Keyboard");
		float v = Input.GetAxisRaw ("Vertical_Keyboard");
		
		Move (h, v);
		Turning ();
		Animating (h, v);
		
		if((dodgeTimer >= timeBetDodge && Input.GetMouseButtonDown(1) && dodgeInit == true) || dodgeInit == false){
			Debug.Log("dodge start");
			Dodge(h, v);
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

		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit floorHit;

		if(Physics.Raycast(camRay, out floorHit, 100f)){
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;
			Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
			playerRigidbody.MoveRotation(newRotation);
		}
	}
	
	void Dodge(float h, float v){
		dodgeTimer = 0f;
		anim.SetTrigger ("dodge");
		
		dodgeDir.Set (h, 0f, v);
		dodgeDir = dodgeDir.normalized;
		
		if(dodgeInit){
			Debug.Log("Initalize dodge");;
			dodgeInit = false;
		}
		else{
			dodgeInTimer += Time.deltaTime;
		}
		
		if(dodgeInTimer > 0.2f){
			Debug.Log("dodge complete");
			//anim.SetBool("IsDodging", false);
			dodgeInTimer = 0;
			dodgeInit = true;
		}
		else{
			Vector3 dodge = dodgeDir * dodgeSpeed * Time.deltaTime;
			playerRigidbody.MovePosition (transform.position + dodge);
		}
		
	}
	
	void Animating(float h, float v){
		bool walking = h != 0f || v != 0f;
		anim.SetBool ("IsWalking", walking);
	}
	
	
	
	
}
