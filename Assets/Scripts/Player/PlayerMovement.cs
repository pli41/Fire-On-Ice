using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6f;
	public float timeBetDodge = 1f;
	public float dodgeDist = 3f;
	public float dodgeSpeed = 15f;

	Vector3 dodgeDir;
	private Rigidbody rigid;
	private Vector3 dodgePos;
	private float dodgeTimer;
	private Vector3 movement;
	private Animator anim;
	private Rigidbody playerRigidbody;
	private int floorMask;
	private float camRayLength = 100f;
	private bool dodgeInit;

	void Awake(){
		rigid = GetComponent<Rigidbody> ();
		floorMask = LayerMask.GetMask("Floor");
		anim = GetComponent<Animator> ();
		playerRigidbody = GetComponent<Rigidbody> ();
		dodgeInit = false;
	}

	void FixedUpdate(){
		dodgeTimer += Time.deltaTime;

		float h = Input.GetAxisRaw ("PS4_Horizontal");
		float v = Input.GetAxisRaw ("PS4_Vertical");

		if(anim.GetBool("IsDodging")){
			Dodge(h, v);
		}
		else{
			Move (h, v);
			Turning ();
			Animating (h, v);
			
			if(dodgeTimer >= timeBetDodge && Input.GetAxisRaw("PS4_L2") > 0){
				Debug.Log("dodge start");
				Dodge(h, v);
				dodgeInit = true;
			}
		}



	}

	void Move(float h, float v){
		movement.Set (h, 0f, v);
		movement = movement.normalized * speed * Time.deltaTime;
		playerRigidbody.MovePosition (transform.position + movement);
	}

	void Turning(){


		float hori = Input.GetAxis("PS4_RightAnalogHorizontal");
		float vert = Input.GetAxis("PS4_RightAnalogVertical");

		if(hori != 0 && vert != 0){
			Vector3 direction = new Vector3 (hori, 0f, vert);
			
			Quaternion newRotation = Quaternion.LookRotation(direction);
			playerRigidbody.MoveRotation(newRotation);
			Debug.Log ("hori = " + hori);
			Debug.Log ("vert = " + vert);
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

	void Dodge(float h, float v){
		dodgeTimer = 0f;
		anim.SetBool ("IsDodging", true);

		dodgeDir.Set (h, 0f, v);
		dodgeDir = dodgeDir.normalized;

		if(dodgeInit){
			Debug.Log("Initalize dodge");
			Vector3 dodgeMov = dodgeDir * dodgeDist;
			dodgePos = transform.position + dodgeMov;
			dodgeInit = false;
		}

		if(Vector3.Distance(transform.position, dodgePos) < 0.5f){
			Debug.Log("dodge complete");
			anim.SetBool("IsDodging", false);
			dodgePos = transform.position;
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
