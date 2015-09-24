using UnityEngine;
using System.Collections;

public class Blink : Ability, MovementEffect, Cooldown {

	public float flashDistance;
	public float cooldown;

	private GameObject playerModel;
	private Vector3 targetPos_final;
	private float characterSize;
	private int ignoreLayer;

	// Use this for initialization
	void Start () {
		triggerOnce = true;
		playerModel = owner.transform.Find ("PlayerModel").gameObject;
		ignoreLayer = LayerMask.NameToLayer ("Obstacles");
		characterSize = 0.5f;
		SetupCooldown ();
	}

	public void SetupCooldown(){
		cooldown_new = cooldown;
		cdTimer = 0f;
	}

	public void ResetCooldown(){
		cdTimer = 0f;
		abilityReady = false;
	}

	public void CooldownUpdate(){
		if(cdTimer < cooldown_new){
			cdTimer += Time.deltaTime;
		}
		else{
			abilityReady = true;
		}
	}

	// Update is called once per frame
	void Update () {
		CooldownUpdate ();
	}


	public override void Cast(){
		if(abilityReady){
			Debug.Log ("Casting blink");
			targetPos_final = Move();
			if(targetPos_final.magnitude > 0){
				owner.GetComponent<Rigidbody>().position = targetPos_final;
				owner.GetComponent<Rigidbody>().useGravity = false;
				playerModel.SetActive(false);
				owner.GetComponent<Collider>().enabled = false;
			}
			else{
				Debug.Log("No target position found");
			}
		}
		else{
			Debug.Log("Blink not ready");
		}
	}
	
	public override void EndCast(){
		Debug.Log ("End-casting blink");
		if(abilityReady){
			playerModel.SetActive(true);
			owner.GetComponent<Collider>().enabled = true;
			owner.GetComponent<Rigidbody>().useGravity = true;
			ResetCooldown ();
		}


	}
	
	public override void SetupObj(){}



	public Vector3 Move(){
		RaycastHit hit;
		Vector3 targetPos = new Vector3();
		if (Physics.Raycast(owner.transform.position, owner.transform.forward, out hit, flashDistance, ignoreLayer)) {
			print("There is something in front of the object!");
			targetPos = owner.transform.position + owner.transform.forward * (hit.distance-characterSize);
		}
		else{
			targetPos = owner.transform.position + owner.transform.forward * flashDistance;
		}
		return targetPos;
	}

	public void EndMove(){

	}
}
