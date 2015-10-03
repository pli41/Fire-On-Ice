using UnityEngine;
using System.Collections;

public class DieWater : MonoBehaviour {

	public float dmgTime = 2f;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if(col.tag == "Player"){
			col.gameObject.GetComponent<PlayerHealth>().currentHealth = 0;
			col.gameObject.SetActive(false);
		}
		else{
			Destroy(col.gameObject);
		}
	}
}
