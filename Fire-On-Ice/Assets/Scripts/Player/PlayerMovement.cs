using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	//start from 1
	private int joystickNum;
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
	private Animation anim;
	private Rigidbody playerRigidbody;

	public float maxSpeed;
	public float oldMaxSpeed;
	private GameManager gm;

	void Start(){
		oldMaxSpeed = maxSpeed;
		gm = GameObject.Find ("GameManager").GetComponent<GameManager>();
		joystickNum = GetComponent<PlayerAttack>().joystickNum;
		anim = GetComponent<Animation> ();
		playerRigidbody = GetComponent<Rigidbody> ();
		canMove = true;
		canTurn = true;
		SetupAnimations ();
	}

	void SetupAnimations(){
		foreach(AnimationState animState in anim){
			if(animState.clip.name == "Attack1"){
				//animState.speed = 3f;
			}
			if(animState.clip.name == "Attack2"){
				//animState.speed = 3f;
			}
		}
	}

	void Update(){

		if(gm.GameInProgress){


			dodgeTimer += Time.deltaTime;
			
			float h = ControllerManager.GetAxis (ControllerInputWrapper.Axis.LeftStickX, joystickNum, true);
			float v = ControllerManager.GetAxis (ControllerInputWrapper.Axis.LeftStickY, joystickNum, true);
			
			//Debug.Log ("h = " + h + " v = " + v);
			if(!disabled){
				if(canMove || canTurn){
					if(canMove){
						maxSpeed = oldMaxSpeed;
						Move (h, v);
					}
					else{
						speed = 0f;
						maxSpeed = 0f;
					}
					if(canTurn){
						Turning ();
					}
				}
			}
			else{
				maxSpeed = 0;
				speed = 0;
				playerRigidbody.velocity = Vector3.zero;
			}
			Animating (h, v);
			//		if((dodgeTimer >= timeBetDodge && Input.GetAxisRaw("PS4_L2_" + playerString) > 0 && dodgeInit == true) || dodgeInit == false){
			//			Debug.Log("dodge start");
			//			Dodge(h, v);
			//		}
		}

	}
	
	void Move(float h, float v){
		movement.Set (h, 0f, v);
		float acc = Mathf.Sqrt (h * h + v * v);

		speed = acc * maxSpeed / 0.09f;

		if(speed < minSpeed){
			speed = minSpeed;
		}
		if(speed > maxSpeed){
			speed = maxSpeed;
		}
        //Debug.Log(acc);
		movement = movement.normalized * speed * acc * accFactor;
		
		if(h != 0 || v != 0){
			if(playerRigidbody.velocity.magnitude < maxSpeed){
				playerRigidbody.velocity += movement;
			}
			else{
				playerRigidbody.velocity = playerRigidbody.velocity.normalized * maxSpeed;
			}	
		}

		//Handle turning while moving, Turning method has a higher priority
		if(canTurn){
			if(Mathf.Abs(h) > 0.01f || Mathf.Abs(v) > 0.01f){
				//Debug.Log ("Turning by left analog");
				Vector3 direction = new Vector3 (h, 0f, v);
				Quaternion newRotation = Quaternion.LookRotation(direction, transform.up);
				transform.rotation = newRotation; 
					//Quaternion.Lerp (transform.rotation, newRotation, Time.deltaTime * 100f);
			}
		}

	}
	
	void Turning(){
		float hori = ControllerManager.GetAxis(ControllerInputWrapper.Axis.RightStickX, joystickNum, true);
		float vert = ControllerManager.GetAxis(ControllerInputWrapper.Axis.RightStickY, joystickNum, true);
		
		if(Mathf.Abs(hori) > 0.01f || Mathf.Abs(vert) > 0.01f){
			//Debug.Log ("Turning by right analog");
			Vector3 direction = new Vector3 (hori, 0f, vert);
			Quaternion newRotation = Quaternion.LookRotation(direction, transform.up);
			transform.rotation = newRotation;
				//Quaternion.Lerp (transform.rotation, newRotation, Time.deltaTime * 100f);


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
		bool moving = (h != 0f || v != 0f) && canMove && speed > 0.01f;
		if(moving){
			if(speed <= 3f && speed > 0){
				anim.CrossFade("Walk");
				//Debug.Log("Walk");
			}
			else{
				anim.CrossFade("Run", 0.1f);
				//Debug.Log("Run");
			}
		}
		else{
			//Debug.Log(anim.clip.name);
			anim.CrossFadeQueued("Idle", 0.1f);
		}
	}
	
	
	
	
}
