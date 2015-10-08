using UnityEngine;
using System.Collections;

public class MeltingIsland : MonoBehaviour {

	public float colorChange = 0.1f;
	public float scaleChange = 0.1f;
	public float meltTime1 = 2f;
	public float meltTime2 = 2f;
	public float meltTimeKey = 2f;
	public bool active;

	MeshRenderer mr;
	float timer1;
	float timer2;
	float timerKey;
	Material mat;

	// Use this for initialization
	void Start () {
		mat = GetComponent<MeshRenderer> ().material;
		active = true;
		mr = GetComponent<MeshRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(!active){
			mr.enabled = false;
		}
	}

	void OnTriggerStay(Collider col){

		if(active){
			if(col.gameObject.tag == "Player"){
				//Debug.Log("Player detected");
				PlayerHealth PH = col.gameObject.GetComponent<PlayerHealth>();

				if(col.gameObject.name == "Player_PS4_1"){
					if(PH.onFire){
						meltTime1 = 0.3f;
					}
					else{
						meltTime1 = 2f;
					}
					
					if(timer1 > meltTime1){
						timer1 = 0;
						Melt(PH.onFire);
					}
					else{
						timer1 += Time.deltaTime;
					}
				}
				else if(col.gameObject.name == "Player_PS4_2"){
					if(PH.onFire){
						meltTime2 = 0.3f;
						//Debug.Log("Fast melting");
					}
					else{
						meltTime2 = 2f;
						//Debug.Log("Melting");
					}
					
					if(timer2 > meltTime2){
						timer2 = 0;
						Melt(PH.onFire);
					} 
					else{
						timer2 += Time.deltaTime;
					}
				}

			}
			else if (col.gameObject.tag == "Player_Key"){

				PlayerAttack_Key PA = col.gameObject.GetComponent<PlayerAttack_Key>();
				
				if(PA.onFire){
					meltTimeKey = 0.3f;
					//Debug.Log("Fast melting");
				}
				else{
					meltTimeKey = 2f;
					//Debug.Log("Melting");
				}
				
				if(timerKey > meltTimeKey){
					timerKey = 0;
					Melt(PA.onFire);
				} 
				else{
					timerKey += Time.deltaTime;
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
			active = false;
		}
		transform.localScale = scale;
	}

	public void meltByExplode(int amount){
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
