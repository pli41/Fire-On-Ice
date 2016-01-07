using UnityEngine;
using System.Collections;

public class Cyclone_Object : MonoBehaviour {

	public Ability ability;

	public float speed;
	public float acceleration;
	public float maxSpeed;
	public float damage;
	public float affectRadius;
	public float forceFactor;
	public float meltFactor;
	public float meltTime;

	private float meltTimer;
	private Rigidbody rigid;
	private CapsuleCollider col;
	private bool meltReady;

	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody> ();

		col = GetComponent<CapsuleCollider> ();
		col.radius = affectRadius;

	}



	// Update is called once per frame
	void Update () {
		if (meltTimer < meltTime) {
			meltTimer += Time.deltaTime;
		} 
		else {
			meltReady = true;
		} 

		Vector3 deltaSpeed = transform.forward * acceleration * Time.deltaTime;
		//Debug.Log (deltaSpeed);
		if(rigid.velocity.magnitude < maxSpeed){
			//Debug.Log("Speeding up");
			rigid.velocity += deltaSpeed;
		}
		else{
			//Debug.Log("full speed");
		}
	}

	void OnTriggerStay(Collider col){
		if(meltReady){
			if(col.tag == "Island"){
				col.GetComponent<MeltingIsland>().meltByExplode(meltFactor);
			}
			else if(col.tag == "Island_New"){
				col.GetComponent<MeltingIsland_New>().meltByExplode(meltFactor);
			}
			else if(col.tag == "Player"){
				Vector3 cyclonePos = transform.position;
				Vector3 playerPos = col.transform.position;
				Vector3 direction = (cyclonePos - playerPos).normalized;
				direction.Set(direction.x, 0f, direction.z);
				
				Vector3 cyclonePos_hori = new Vector3(cyclonePos.x, 0, cyclonePos.z);
				Vector3 playerPos_hori = new Vector3(playerPos.x, 0, playerPos.z);
				
				float horizontalDistance = Vector3.Distance(cyclonePos_hori, playerPos_hori);

				if(affectRadius > horizontalDistance){
					col.attachedRigidbody.AddForce(direction * (affectRadius - horizontalDistance) * forceFactor);
					col.GetComponent<PlayerHealth>().TakeDamage(damage * (affectRadius - horizontalDistance), true, 
					                                            ability.owner.GetComponent<PlayerAttack>().playerNum);
				}

				//Debug.Log("Tornado damage: " + damage * (affectRadius - horizontalDistance));
			}
			else if(col.tag == "Boundary"){
				Destroy(gameObject);
			}
		}
	}


	void OnTriggerEnter(Collider col){
		if(col.tag == "Boundary"){
			Destroy(gameObject, 2f);
		}
	}

	void ResetMeltTimer(){
		meltReady = false;
		meltTimer = 0;
	}



}
