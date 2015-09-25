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

	void OnTriggerEnter(Collider col){
		if(col.tag == "Player"){
			col.gameObject.SetActive(false);
		}
	}
}
