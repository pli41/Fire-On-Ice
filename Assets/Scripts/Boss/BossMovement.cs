using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossMovement : MonoBehaviour {

	public float speed;
	public float accFactor;
	public GameObject[] playersList;
	public float chargeTargetTime;
	public float rotateSpeed;
	public float meleeRange;
	public float obstacleForce;

	private Animator anim;
	private Rigidbody rigid;
	private Vector3 movement;
	public GameObject currentTarget;
	private float chasingTimer;
	private bool locatePlayer;
	private bool chasing;
	private List<GameObject> players;

	// Use this for initialization
	void Start () {
		players = new List<GameObject> ();
		playersList = GameObject.FindGameObjectsWithTag ("Player");
		rigid = GetComponent<Rigidbody> ();
		currentTarget = FindTarget ();
		SetupArraylist ();
	}

	void SetupArraylist(){
		foreach(GameObject g in playersList){
			players.Add(g);
		}
	}

	// Update is called once per frame
	void Update () {
		CheckAllPlayers ();
		if(currentTarget != null){
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
			currentTarget = FindTarget();
			chasing = false;
		}


		if(chasingTimer < chargeTargetTime){
			chasingTimer += Time.deltaTime;
		}
		else{
			chasingTimer = 0f;
			currentTarget = null;
			Debug.Log("Change target");
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
		for(int i = 0; i < players.Count; i++){
			if(!players[i].activeInHierarchy){
				players.RemoveAt(i);
			}
		}
	}

	GameObject FindTarget(){
		if(players.Count > 0){
			Random.seed = (int)(Random.value * 50f);
			float random = (float)Random.value;
			//Debug.Log ("Random = " + random);
			int playerSelectedNum = (int)(random / (1f / (float)players.Count));
			//Debug.Log ("Selected player " + playerSelectedNum);
			if(!players[playerSelectedNum].activeInHierarchy){
				return FindTarget();
			}
			else{
				rigid.velocity = Vector3.zero;
				return players[playerSelectedNum];
			}
		}
		else{
			//Debug.Log ("Players all died");
			rigid.velocity = Vector3.zero;
			return null;
		}
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
