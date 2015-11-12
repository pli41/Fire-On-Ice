using UnityEngine;
using System.Collections;

public class Blink : Ability, MovementEffect, Cooldown {

	public float flashDistance;

	private GameObject playerModel;
	private Vector3 targetPos_final;
	public float characterSize;
	private int ignoreLayer;
	private AudioSource audio;
	private bool createdBlinkObj;
	private bool foundFinalPos;
	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
		handledEndCast = true;
		playerModel = owner.transform.Find ("PlayerModel").gameObject;
		ignoreLayer = LayerMask.NameToLayer ("Obstacles");
		characterSize = 0.5f;
		SetupCooldown ();
	}

	public void SetupCooldown(){
		cdTimer = 0f;
	}

	public void ResetCooldown(){
		cdTimer = 0f;
		abilityReady = false;
	}

	public void CooldownUpdate(){
		if(cdTimer < cooldown){
			cdTimer += Time.deltaTime;
		}
		else{
			abilityReady = true;
		}
	}

	// Update is called once per frame
	void Update () {
		timeUntilReset = (int)(cooldown - cdTimer + 1f);
		CooldownUpdate ();
	}


	public override void Cast(){

		if(abilityReady){
			if(!foundFinalPos){
				targetPos_final = Move();
				foundFinalPos = true;
				Gizmos.DrawSphere(targetPos_final, 2);
			}
			Debug.Log ("Casting blink");

			if(targetPos_final.magnitude > 0){
				//create ability object

				//ResetCooldown();
				if(!createdBlinkObj){
					createdBlinkObj = true;
					GameObject blinkObject = Instantiate(ability_object);
					blinkObject.transform.position = owner.GetComponent<Rigidbody>().position;
					Destroy(blinkObject, 2f);
				}


				owner.transform.position = targetPos_final;
				owner.GetComponent<Rigidbody>().useGravity = false;
				playerModel.SetActive(false);
				owner.GetComponent<Collider>().enabled = false;
				Invoke("EndCast", 0.1f);

				if(!audio.isPlaying){
					audio.Play();
				}

			}
			else{
				Debug.Log("No target position found");
			}

			if(Vector3.Distance(targetPos_final, transform.position) < 0.01f){
				EndCast();
			}
		}
		else{
			Debug.Log("Blink not ready");
		}
	}
	
	public override void EndCast(){
		Debug.Log ("End-casting blink");
		playerModel.SetActive(true);
		createdBlinkObj = false;
		foundFinalPos = false;
		owner.GetComponent<Collider>().enabled = true;
		owner.GetComponent<Rigidbody>().useGravity = true;
		ResetCooldown ();


	}
	
	public override void SetupObj(){}
	
	public Vector3 Move(){
//		RaycastHit hit;
		Vector3 targetPos = new Vector3();
//		if (Physics.Raycast(owner.transform.position, owner.transform.forward, out hit, flashDistance, ignoreLayer)) {
//			print("There is something in front of the object!");
//			targetPos = owner.transform.position + owner.transform.forward * (hit.distance-characterSize);
//		}
//		else{
//			print("Open Blink");
			targetPos = owner.transform.position + owner.transform.forward * flashDistance;
//		}
		return targetPos;
	}

	public void EndMove(){

	}
}
