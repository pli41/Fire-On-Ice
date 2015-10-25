using UnityEngine;
using System.Collections;

public class MeltingIsland : MonoBehaviour {

	public float colorChange = 0.1f;
	public float scaleChange = 0.1f;
	public bool active;

	MeshRenderer mr;

	float timerKey;
	Material mat;
	GameObject[] players;
	float[] timers;
	float[] meltTimes;
	GameManager gm;
	bool initialized;
	// Use this for initialization
	void Start () {
		gm = GameObject.Find ("GameManager").GetComponent<GameManager>();
		mat = GetComponent<MeshRenderer> ().material;
		active = true;
		mr = GetComponent<MeshRenderer> ();
		Initialize ();
	}
	
	void Initialize(){
		players = GameObject.FindGameObjectsWithTag ("Player");
		timers = new float[players.Length];
		meltTimes = new float[players.Length];
		for (int i = 0; i < 0; i++){
			meltTimes[i] = 2f;
		}
	}

	// Update is called once per frame
	void Update () {

		if(gm.enablePlayers && !initialized){
			Initialize();
			initialized = true;
		}
	}

	void OnTriggerStay(Collider col){

		if(active){
			if(col.gameObject.tag == "Player"){
				//Debug.Log("Player detected");

				PlayerHealth PH = col.gameObject.GetComponent<PlayerHealth>();
				int playerNum = col.gameObject.GetComponent<PlayerAttack>().joystickNum - 1;

				if(PH.onFire){
					meltTimes[playerNum] = 0.3f;
				}
				else{
					meltTimes[playerNum] = 2f;
				}

				if(timers[playerNum] > meltTimes[playerNum]){
					timers[playerNum] = 0f;
					Melt(PH.onFire);
				}
				else{
					timers[playerNum] += Time.deltaTime;
				}
			}
		}

	}

	void Melt(bool onFire){
		//change color

		Color color = mat.color;
		color.g -= colorChange;
		color.r -= colorChange;
		mat.color = color;

		//change thickness
		Vector3 scale = transform.localScale;
		scale.y -= scaleChange;

		if(scale.y < 0.1f){
			Destroy(gameObject);
		}
		transform.localScale = scale;
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
