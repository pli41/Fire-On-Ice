using UnityEngine;
using System.Collections;

public class DieWater : MonoBehaviour {

	public float dmgTime = 2f;

	float timer;
	// Use this for initialization
	void Start () {
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider col){
		if(col.gameObject.tag == "Player1"){
			Debug.Log("GG");
			PlayerHealth_1 PH = col.gameObject.GetComponent<PlayerHealth_1>();
			if(timer > dmgTime){
				timer = 0;
				PH.TakeDamage(10);
			}
			else{
				timer += Time.deltaTime;
			}

		}
		else if(col.gameObject.tag == "Player2"){
			Debug.Log("GG");
			PlayerHealth_2 PH = col.gameObject.GetComponent<PlayerHealth_2>();
			if(timer > dmgTime){
				timer = 0;
				PH.TakeDamage(10);
			}
			else{
				timer += Time.deltaTime;
			}

		}
	}
}
