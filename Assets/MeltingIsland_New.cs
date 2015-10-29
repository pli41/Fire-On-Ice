using UnityEngine;
using System.Collections;

public class MeltingIsland_New : MonoBehaviour {

	//For Emily's new island

	public float colorChange = 0.1f;
	public float scaleChange;
	public bool active;
	public float meltTime;

	MeshRenderer mr;
	
	float timerKey;
	Material mat;
	GameObject[] players;
	float timer;
	float currentMeltTime;
	bool meltReady;
	bool occupiedByPlayer;

	bool initialized;
	
	GameManager gm;
	
	// Use this for initialization
	void Start () {
		
		gm = GameObject.Find ("GameManager").GetComponent<GameManager>();
		mat = GetComponent<MeshRenderer> ().material;
		
		mr = GetComponent<MeshRenderer> ();
		currentMeltTime = meltTime;
		//Initialize ();
	}
	
//	void Initialize(){
//		
//		players = GameObject.FindGameObjectsWithTag ("Player");
//		timers = new float[players.Length];
//		meltTimes = new float[players.Length];
//
//		for (int i = 0; i < meltTimes.Length; i++){
//			meltTimes[i] = 2f;
//		}
//
//		initialized = true;
//	}
	
	// Update is called once per frame
	void Update () {
		active = gm.GameInProgress;

		if(!meltReady && occupiedByPlayer){
			if(timer >= currentMeltTime){
				meltReady = true;
				timer = 0f;
			}
			else{
				timer += Time.deltaTime;
			}
		}
	}

	void OnCollisionEnter(Collision col){
		if(active){
			if(col.gameObject.tag == "Player"){
				occupiedByPlayer = true;
			}
		}
	}

	void OnCollisionStay(Collision col){
		Debug.Log ("Collision Detected");

		if(active){
			if(col.gameObject.tag == "Player"){
				Debug.Log("Player detected");
				occupiedByPlayer = true;
				PlayerHealth PH = col.gameObject.GetComponent<PlayerHealth>();
				//int playerNum = col.gameObject.GetComponent<PlayerAttack>().joystickNum - 1;
				
				if(PH.onFire){
					currentMeltTime = meltTime / 5f;
				}
				else{
					currentMeltTime = meltTime;
				}
				
				if(meltReady){
					Melt();
				}
			}
		}
	}

	void OnCollisionExit(Collision col){
		if(active){
			if(col.gameObject.tag == "Player"){
				occupiedByPlayer = false;
			}
		}
	}
	
	void Melt(){
		//change color
		Debug.Log ("Melting");
		Color color = mat.color;
		color.g -= colorChange;
		color.r -= colorChange;
		mat.color = color;
		
		//change thickness
		Vector3 scale = transform.localScale;
		scale.z -= scaleChange;

		if(scale.z <= 0f){
			Destroy(gameObject);
		}

		transform.localScale = scale;
		meltReady = false;
	}
	
	public void meltByExplode(float amount){
		//change color
		Color color = mat.color;
		color.g -= colorChange * (float)amount / 5f;
		color.r -= colorChange * (float)amount / 5f;
		mat.color = color;
		
		//change thickness
		Vector3 scale = transform.localScale;
		scale.y -= scaleChange * (float)amount / 5f;
		
		if(scale.y < 0.1f){
			Destroy(gameObject);
			active = false;
		}
		transform.localScale = scale;
	}
}
