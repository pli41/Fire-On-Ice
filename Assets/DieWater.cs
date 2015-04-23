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
		if(col.gameObject.tag == "Player"){
			Debug.Log("GG");
			PlayerHealth PH = col.gameObject.GetComponent<PlayerHealth>();
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
