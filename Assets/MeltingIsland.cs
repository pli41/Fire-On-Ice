using UnityEngine;
using System.Collections;

public class MeltingIsland : MonoBehaviour {

	public float colorChange = 0.1f;
	public float scaleChange = 0.1f;
	public float meltTime1 = 2f;
	public float meltTime2 = 2f;
	public bool active;

	MeshRenderer mr;
	float timer1;
	float timer2;
	Material mat;

	// Use this for initialization
	void Start () {
		mat = GetComponent<MeshRenderer> ().material;
		active = true;
		mr = GetComponent<MeshRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider col){

		if(active){
			if(col.gameObject.tag == "Player1"){
				//Debug.Log("Player detected");
				PlayerAttack_1 PA = col.gameObject.GetComponent<PlayerAttack_1>();
				
				if(PA.onFire){
					meltTime1 = 0.6f;
					Debug.Log("Fast melting");
				}
				else{
					meltTime1 = 2f;
					Debug.Log("Melting");
				}
				
				if(timer1 > meltTime1){
					timer1 = 0;
					Melt(PA.onFire);
				}
				else{
					timer1 += Time.deltaTime;
				}
			}
			else if (col.gameObject.tag == "Player2"){
				PlayerAttack_2 PA = col.gameObject.GetComponent<PlayerAttack_2>();
				
				if(PA.onFire){
					meltTime2 = 0.6f;
					//Debug.Log("Fast melting");
				}
				else{
					meltTime2 = 2f;
					//Debug.Log("Melting");
				}
				
				if(timer2 > meltTime2){
					timer2 = 0;
					Melt(PA.onFire);
				}
				else{
					timer2 += Time.deltaTime;
				}
			}
		}
		else{
			mr.enabled = false;
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

		if(scale.y < 0){
			scale.y = 0;
			active = false;
		}
		transform.localScale = scale;
	}

	public void meltByExplode(int amount){
		//change color
		Color color = mat.color;
		color.g -= colorChange * (float)amount / 10f;
		color.r -= colorChange * (float)amount / 10f;
		mat.color = color;
		
		//change thickness
		Vector3 scale = transform.localScale;
		scale.y -= scaleChange * (float)amount / 10f;
		
		if(scale.y < 0){
			scale.y = 0;
			active = false;
		}
		transform.localScale = scale;
	}


}
