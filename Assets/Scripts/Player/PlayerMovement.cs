using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6f;
	public float timeBetDodge = 1f;
	public float dodgeDist = 3f;
	public float dodgeSpeed = 15f;

	private Vector3 dodgePos;
	private float dodgeTimer;
	private Vector3 movement;
	private Animator anim;
	private Rigidbody playerRigidbody;
	private int floorMask;
	private float camRayLength = 100f;


	void Awake(){
		floorMask = LayerMask.GetMask("Floor");
		anim = GetComponent<Animator> ();
		playerRigidbody = GetComponent<Rigidbody> ();
	}

	void FixedUpdate(){
		dodgeTimer += Time.deltaTime;

		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		if(anim.GetBool("IsDodging")){
			Dodge();
		}
		else{
			Move (h, v);
			Turning ();
			Animating (h, v);
			
			if(dodgeTimer >= timeBetDodge && Input.GetKey(KeyCode.Space)){
				Dodge();
			}
		}



	}

	void Move(float h, float v){
		movement.Set (h, 0f, v);
		movement = movement.normalized * speed * Time.deltaTime;
		playerRigidbody.MovePosition (transform.position + movement);
	}

	void Turning(){
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

		if(dodgePos == transform.position){
			Vector3 dodgeMov = transform.forward * dodgeDist;
			dodgePos = transform.position + dodgeMov;
		}

		if(Vector3.Distance(transform.position, dodgePos) < 2f){
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
