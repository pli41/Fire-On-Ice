using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossMovement : MonoBehaviour {

	public float speed;
	public float accFactor;
	public GameObject[] players;
	public float chargeTargetTime;
	public float rotateSpeed;
	public float meleeRange;
	public float obstacleForce;
	public bool disabled;


	private Animator anim;
	private Rigidbody rigid;
	private Vector3 movement;
	public GameObject currentTarget;
	private float chasingTimer;
	private bool locatePlayer;
	private bool chasing;
	private List<GameObject> playersList;

	// Use this for initialization
	void Start () {
		playersList = new List<GameObject> ();
		players = GameObject.FindGameObjectsWithTag ("Player");
		rigid = GetComponent<Rigidbody> ();
		SetupList ();
		//currentTarget = FindTarget ();
	}

	void SetupList(){
		foreach(GameObject g in players){
			playersList.Add(g);
		}
	}

	// Update is called once per frame
	void Update () {
		CheckAllPlayers ();
		if(!disabled){
			if(currentTarget != null && currentTarget.activeInHierarchy){
				Debug.Log("Current target is not null and it is active");
				if(chasing){
					Debug.Log("Chasing");
					Move ();
					Turn ();
				}
				else{
					Debug.Log("Turning");
					Turn ();
				}
			}
			else{
				Debug.Log("Find target because current is not available.");
				Debug.Break();
				chasing = false;
				currentTarget = FindTarget();
			}
			
			
			if(chasingTimer < chargeTargetTime){
				chasingTimer += Time.deltaTime;
			}
			else{
				chasingTimer = 0f;
				chasing = false;
				Debug.Log("Time to change target");
				currentTarget = FindTarget();
			}
		}
		else{
			rigid.velocity = Vector3.zero;
		}
	}


	void Move() {
		Vector3 bossPos = transform.position;
		Vector3 targetPos = currentTarget.transform.position;
		bossPos.Set (bossPos.x, 0, bossPos.z);
		targetPos.Set (targetPos.x, 0, targetPos.z);

		if(Vector3.Distance(bossPos, targetPos) < meleeRange){
			rigid.velocity = Vector3.zero;
		}
		else{
			movement = currentTarget.transform.position - transform.position;
			movement.Set (movement.x, 0, movement.z);
			movement = movement.normalized * speed * accFactor;
			if(rigid.velocity.magnitude < speed){
				rigid.velocity += movement;
			}
		}
	}
	
	void Turn() {
		Vector3 direction = currentTarget.transform.position - transform.position;
		direction.Set (direction.x, 0, direction.z);
		if(Vector3.Angle(transform.forward, direction) < 10.0f){
			chasing = true;
		}
		float step = rotateSpeed * Time.deltaTime;
		Vector3 newDir = Vector3.RotateTowards (transform.forward, direction, step, 0.0f);
		transform.rotation = Quaternion.LookRotation(newDir);
	}

	void CheckAllPlayers(){
		for(int i = 0; i < playersList.Count; i++){
			if(!playersList[i].activeInHierarchy){

				if(playersList[i].Equals(currentTarget)){
					Debug.Log("Set current Target to be null");
					currentTarget = null;
					//Debug.Break();
				}
				playersList.RemoveAt(i);
			}
		}
	}

	GameObject FindTarget(){
		GameObject result = null;
		while(result == null && playersList.Count > 0){
			Random.seed = (int)(Random.value * 100f);
			int random = Random.Range (0, playersList.Count-1);
			if(playersList[random].activeInHierarchy){
				rigid.velocity = Vector3.zero;
				chasing = true;
				chasingTimer = 0f;
				result = playersList[random];
			}
		}

		if(playersList.Count <= 0){
			Debug.Log ("Players all died");
			disabled = true;
		}
		return result;
	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag == "Obstacle"){
			//Debug.Log("Force");
			Vector3 direction = col.gameObject.transform.position - transform.position;
			direction.Set(direction.x, 0f, direction.z);
			col.rigidbody.AddForce(direction.normalized * obstacleForce);
		}
	}
}
